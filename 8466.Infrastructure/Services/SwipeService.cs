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
        public bool AddSwipe(Swipe swipe)
        {
            _dataContext.Swipes.Add(swipe);
            var added = _dataContext.SaveChanges();

            return added > 0;
        }

        public List<Swipe> GetAllSwipes()
        {
            return _dataContext.Swipes.ToList();
        }
    }
}
