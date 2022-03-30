using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _8466.Domain.Entities
{
    [DataContract]
    public class Operation
    {
        public Operation(string ipAdress)
        {
            IpAddress = ipAdress;
            CurrentStatus = Status.Waiting;
        }
        [DataMember]
        public string IpAddress { get; set; }
        [DataMember]
        public Status CurrentStatus { get; set; }

        public enum Status
        {
            Waiting,
            InProcess,
            Finished
        }
    }
}
