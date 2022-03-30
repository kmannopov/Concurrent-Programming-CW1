using _8466.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8466.Application.Interfaces
{
    public interface ISwipeService
    {
        Task<bool> AddSwipe(Swipe swipe);
        Task<List<Swipe>> GetAllSwipes();
    }
}
