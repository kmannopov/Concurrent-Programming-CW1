using _8466.Application.Interfaces;
using _8466.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
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
            Invoke(new MethodInvoker(delegate
            {
            LoadData();
            }));
        }

        public List<Swipe> GetData()
        {
            return _swipeService.GetAllSwipes();
        }

        public void LoadData()
        {
            var source = new BindingSource();
            var result = GetData();
            source.DataSource = result.ToList();
            dgvAllSwipes.DataSource = source;
        }
    }
}
