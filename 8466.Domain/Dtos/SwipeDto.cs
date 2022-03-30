using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8466.Domain.Dtos
{
    public class SwipeDto
    {
        public SwipeDto(string ipAddress, string studentId, DateTime date, string direction)
        {
            IpAddress = ipAddress;
            StudentId = studentId;
            Date = date;
            Direction = direction;
        }
        public string IpAddress { get; set; }
        public string StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Direction { get; set; }
    }
}
