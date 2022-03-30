using _8466.Domain.Entities;
using System.Collections.Generic;

namespace _8466.Application.Interfaces
{
    public interface ISwipeService
    {
        bool AddSwipes(List<Swipe> swipes);
        List<Swipe> GetAllSwipes();
    }
}
