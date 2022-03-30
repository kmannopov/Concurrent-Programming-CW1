using _8466.Application.Interfaces;
using _8466.Domain.Entities;
using _8466.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcProg_CW1_8466
{
    public partial class AllSwipes : Form
    {
        private readonly ISwipeService _swipeService;
        public AllSwipes()
        {
            InitializeComponent();
            _swipeService = (ISwipeService)Program.ServiceProvider.GetService(typeof(ISwipeService));
            CreateHandle();
            LoadData();
        }

        public async Task<List<Swipe>> GetDataAsync()
        {
            return await _swipeService.GetAllSwipes();
        }

        public async void LoadData()
        {
            var source = new BindingSource();
            var result = await GetDataAsync();
            source.DataSource = result.ToList();
            dgvAllSwipes.DataSource = source;
            foreach (var swipe in result)
            {
                dgvAllSwipes.Rows.Add(swipe);
            }
        }
    }
}
