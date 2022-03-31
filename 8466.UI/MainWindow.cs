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
        private Guid[] guids;
        private List<Operation> operations;
        private AllSwipes AllSwipesForm;
        private Thread refresher;
        public MainWindow()
        {
            webService = new MainServiceReference.MainServiceClient();
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (operations == null)
                ProcessTerminals();
            else
            {
                if (operations.Where(x => x.CurrentStatus == Operation.Status.InProcess
                || x.CurrentStatus == Operation.Status.Waiting).Count() != 0)
                    MessageBox.Show("Some terminals are still in process. Please wait until they are finished.");
                else
                    ProcessTerminals();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            if (AllSwipesForm is null)
                AllSwipesForm = new AllSwipes();
            try
            {
            AllSwipesForm.Show();
            AllSwipesForm.Focus();
            }
            catch (ObjectDisposedException)
            {
                AllSwipesForm = new AllSwipes();
            }
        }

        private void UpdateDataGridView()
        {
            SetupDataGridView(operations);
            while (operations.Where(x => x.CurrentStatus == Operation.Status.InProcess 
            || x.CurrentStatus == Operation.Status.Waiting).Count() != 0)
            {
                operations = webService.GetStatus(guids).ToList();
                SetupDataGridView(operations);
                Thread.Sleep(100);
                try
                {
                ColorCells();
                }
                catch (Exception)
                {
                    Application.Exit();
                }
                Thread.Sleep(200);
            }
        }

        private void SetupDataGridView(List<Operation> operations)
        {
            var source = new BindingSource();
            source.DataSource = operations;
            try
            {
                if (!dgvMain.IsHandleCreated)
                    Thread.CurrentThread.Abort();
                Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = source;
                    dgvMain.Refresh();
                }));
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void ColorCells()
        {
            Invoke(new MethodInvoker(delegate
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
            }));
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ProcessTerminals()
        {
            guids = webService.StartCollectingSwipes();
            operations = webService.GetStatus(guids).ToList();
            var updateThread = new Thread(UpdateDataGridView);
            updateThread.Start();
        }
    }
}
