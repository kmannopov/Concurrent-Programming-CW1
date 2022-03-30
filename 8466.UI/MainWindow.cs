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
        private readonly ISwipeService _swipeService;
        private List<Operation> operations;
        public MainWindow()
        {
            webService = new MainServiceReference.MainServiceClient();
            _swipeService = (ISwipeService)Program.ServiceProvider.GetService(typeof(ISwipeService));
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            operations = webService.StartCollectingSwipes().ToList();
            SetupDataGridView(operations);
            //UpdateDataGridView(operations);
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            AllSwipes Form = new AllSwipes();
            Form.Show();
            Form.Focus();
        }

        private void UpdateDataGridView(List<Operation> operations)
        {
                operations = webService.GetStatus().ToList();
                Invoke(new MethodInvoker(delegate
                {
                dgvMain.Refresh();
                }));
                Thread.Sleep(1000);
        }

        private void SetupDataGridView(List<Operation> operations)
        {
            var source = new BindingSource();
            source.DataSource = operations;
            dgvMain.DataSource = source;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDataGridView(operations);
        }
    }
}
