using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _8466.Domain.Entities;
using _8466.Infrastructure;
using _8466.Infrastructure.Data;
using _8466.Infrastructure.Services;
using SynConnectDLL;

namespace WebService._8466
{
    public class MainService : IMainService
    {
        private readonly SynConnection _connection;
        private Semaphore _pool = new Semaphore(3, 3);
        public MainService()
        {
            _connection = SynConnection.GetInstance();
        }
        public List<Operation> GetStatus(List<Guid> guids)
        {
            var operations = new List<Operation>();
            foreach (Guid guid in guids)
            {
                using (var dbContext = new DataContext())
                {
                    var operationService = new OperationService(dbContext);
                    operations = operationService.GetOperationsById(guids);
                }
            }
            return operations;
        }

        public Guid[] StartCollectingSwipes()
        {
            var guids = new Guid[10];
            for (int i = 0; i < 10; i++)
            {
                var ipAddress = Extensions.GetRandomIpAddress();
                var operation = new Operation(ipAddress);
                guids[i] = operation.Id;
                var thread = new Thread(() => ProcessSwipes(operation));
                thread.Name = guids[i].ToString();
                thread.Start();
            };

            return guids;
        }

        public void ProcessSwipes(Operation operation)
        {
            using (var dbContext = new DataContext())
            {
                var operationService = new OperationService(dbContext);
                operationService.AddOperation(operation);
            }
            try
            {
                _pool.WaitOne();
                operation.CurrentStatus = Operation.Status.InProcess;

                using (var dbContext = new DataContext())
                {
                    var operationService = new OperationService(dbContext);
                    operationService.UpdateOperation(operation);

                    var swipes = _connection.RetrieveSwipes(operation.IpAddress);
                    var swipeList = swipes.ReturnAsSwipeList(operation.IpAddress);

                    var swipeService = new SwipeService(dbContext);
                    foreach (var swipe in swipeList)
                    {
                        swipeService.AddSwipes(swipeList);
                    }
                }
            }
            finally
            {
                _pool.Release();
                using (var dbContext = new DataContext())
                {
                    var operationService = new OperationService(dbContext);
                    operation.CurrentStatus = Operation.Status.Finished;
                    operationService.UpdateOperation(operation);
                }
            }
        }
    }
}
