using _8466.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8466.Application.Interfaces
{
    public interface IOperationService
    {
        bool AddOperations(IEnumerable<Operation> operations);
        List<Operation> GetOperationsById(List<Guid> guids);
        bool UpdateOperation(Operation updatedOperation);
    }
}
