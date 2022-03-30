using System;

namespace _8466.Domain.Entities
{
    public class Swipe
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string StudentId { get; set; }
        public DateTime SwipeDate { get; set; }
        public string Direction { get; set; }
    }
}
