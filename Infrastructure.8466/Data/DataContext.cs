using Domain._8466.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure._8466.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Swipe> Swipes { get; set; }
    }
}
