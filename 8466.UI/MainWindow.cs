using _8466.Application.Interfaces;
using _8466.Domain.Entities;
using _8466.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcProg_CW1_8466
{
    public partial class MainWindow : Form
    {
        private readonly MainServiceReference.IMainService webService;
        private List<Operation> operations;
        public MainWindow()
        {
            webService = new MainServiceReference.MainServiceClient();
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            operations = webService.StartCollectingSwipes().ToList();
            var thread = new Thread(() => SetupDataGridView(operations));
            thread.Start();
            var updateThread = new Thread(() => UpdateDataGridView(operations));
            updateThread.Start();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            AllSwipes Form = new AllSwipes();
            Form.Show();
            Form.Focus();
        }

        private void UpdateDataGridView(List<Operation> operations)
        {
            while(operations.Where(x => x.CurrentStatus == Operation.Status.InProcess 
            || x.CurrentStatus == Operation.Status.Waiting).Count() != 0)
            {
                var guids = new Guid[operations.Count];
                for (int i = 0; i < operations.Count; i++)
                {
                    guids[i] = operations[i].Id;
                }
                var operationsArray = webService.GetStatus(guids);
                SetupDataGridView(operationsArray.ToList());
                operations = operationsArray.ToList();
                Thread.Sleep(1000);
            }
        }

        private void SetupDataGridView(List<Operation> operations)
        {
            Invoke(new MethodInvoker(delegate
            {
            var source = new BindingSource();
            source.DataSource = operations;
            dgvMain.DataSource = source;
             dgvMain.Refresh();
            }));
        }

        private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                if (row.Cells["CurrentStatus"].Value is null)
                    return;
                if ((Operation.Status)row.Cells["CurrentStatus"].Value == Operation.Status.Waiting)
                {
                    row.Cells["CurrentStatus"].Style.BackColor = Color.LightSalmon;
                }
                else if ((Operation.Status)row.Cells["CurrentStatus"].Value == Operation.Status.InProcess)
                {
                    row.Cells["CurrentStatus"].Style.BackColor = Color.Yellow;
                }
                else if ((Operation.Status)row.Cells["CurrentStatus"].Value == Operation.Status.Finished)
                {
                    row.Cells["CurrentStatus"].Style.BackColor = Color.Green;
                }
            }

        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
