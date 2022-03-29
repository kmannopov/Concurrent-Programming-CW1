using Application._8466.Interfaces;
using Domain._8466.Entities;
using Infrastructure._8466.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure._8466.Services
{
    public class SwipeService : ISwipeService
    {
        private readonly DataContext _dataContext;

        public SwipeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddSwipe(Swipe swipe)
        {
            await _dataContext.Swipes.AddAsync(swipe);
            var added = await _dataContext.SaveChangesAsync();

            return added > 0;
        }

        public async Task<IEnumerable<Swipe>> GetAllSwipes()
        {
            return await _dataContext.Swipes.ToListAsync();
        }
    }
}
