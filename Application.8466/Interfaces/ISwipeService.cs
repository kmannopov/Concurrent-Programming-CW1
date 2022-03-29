using Domain._8466.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application._8466.Interfaces
{
    public interface ISwipeService
    {
        Task<bool> AddSwipe(Swipe swipe);
        Task<IEnumerable<Swipe>> GetAllSwipes();
    }
}
