using _8466.Application.Interfaces;
using _8466.Domain.Entities;
using _8466.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8466.Infrastructure.Services
{
    public class SwipeService : ISwipeService
    {
        private readonly DataContext _dataContext;

        public SwipeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool AddSwipes(List<Swipe> swipes)
        {
            _dataContext.Swipes.AddRange(swipes);
            var added = _dataContext.SaveChanges();

            return added == swipes.Count;
        }

        public List<Swipe> GetAllSwipes()
        {
            return _dataContext.Swipes.ToList();
        }
    }
}
