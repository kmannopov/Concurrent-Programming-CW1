using _8466.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8466.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public DbSet<Swipe> Swipes { get; set; }
    }
}
