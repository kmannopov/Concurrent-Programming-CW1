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
    public class OperationService : IOperationService
    {
        private readonly DataContext _dataContext;
        public OperationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool AddOperation(Operation operation)
        {
            _dataContext.Operations.Add(operation);
            var added = _dataContext.SaveChanges();

            return added > 0;
        }

        public List<Operation> GetOperationsById(List<Guid> guids)
        {
            return _dataContext.Operations.Where(x => guids.Contains(x.Id)).ToList();
        }

        public bool UpdateOperation(Operation updatedOperation)
        {
            var operation = _dataContext.Operations.Where(x => x.Id == updatedOperation.Id).First();
            operation.CurrentStatus = updatedOperation.CurrentStatus;
            var updated = _dataContext.SaveChanges();

            return updated > 0;
        }
    }
}
