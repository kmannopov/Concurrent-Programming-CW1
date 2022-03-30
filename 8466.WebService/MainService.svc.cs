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

        public List<Operation> StartCollectingSwipes()
        {
            var operations = new List<Operation>();
            for (int i = 0; i < 10; i++)
            {
                var ipAddress = Extensions.GetRandomIpAddress();
                var operation = new Operation(ipAddress);
                var thread = new Thread(() => ProcessSwipes(operation));
                thread.Start();
                operations.Add(operation);
            };

            using (var dbContext = new DataContext())
            {
                var operationService = new OperationService(dbContext);
                operationService.AddOperations(operations);
            }
                return operations;
        }

        public void ProcessSwipes(Operation operation)
        {
            try
            {
                _pool.WaitOne();
                operation.CurrentStatus = Operation.Status.InProcess;
                var swipes = _connection.RetrieveSwipes(operation.IpAddress);
                var swipeList = swipes.ReturnAsSwipeList(operation.IpAddress);

                using (var dbContext = new DataContext())
                {
                    var operationService = new OperationService(dbContext);
                    operationService.UpdateOperation(operation);

                    var swipeService = new SwipeService(dbContext);
                    foreach (var swipe in swipeList)
                    {
                        swipeService.AddSwipe(swipe.ReturnAsSwipe());
                    }
                }
                Thread.Sleep(3000);
            }
            finally
            {
                using (var dbContext = new DataContext())
                {
                    var operationService = new OperationService(dbContext);
                    operation.CurrentStatus = Operation.Status.Finished;
                    operationService.UpdateOperation(operation);
                }
                _pool.Release();
            }
        }
    }
}
