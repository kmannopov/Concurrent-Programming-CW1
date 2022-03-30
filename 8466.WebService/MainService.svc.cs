using System;
using System.Collections.Generic;
using System.Threading;
using _8466.Domain.Entities;
using _8466.Infrastructure;
using _8466.Infrastructure.Data;
using _8466.Infrastructure.Services;
using SynConnectDLL;

namespace WebService._8466
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MainService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MainService.svc or MainService.svc.cs at the Solution Explorer and start debugging.
    public class MainService : IMainService
    {
        private readonly SynConnection _connection;
        private Semaphore _pool = new Semaphore(3, 3);
        private Thread[] threads = new Thread[10];
        private List<Operation> operations;
        public MainService()
        {
            _connection = SynConnection.GetInstance();
        }
        public List<Operation> GetStatus()
        {
            return operations;
        }

        public List<Operation> StartCollectingSwipes()
        {
            operations = new List<Operation>();
            for (int i = 0; i < 10; i++)
            {
                var ipAddress = Extensions.GetRandomIpAddress();
                threads[i] = new Thread(() => ProcessSwipes(ipAddress));
                threads[i].Start();
                operations.Add(new Operation(ipAddress));
            };

            return operations;
        }

        public async void ProcessSwipes(string ipAddress)
        {
            try
            {
                _pool.WaitOne();
                operations.Find(x => x.IpAddress == ipAddress).CurrentStatus = Operation.Status.InProcess;
                var swipes = _connection.RetrieveSwipes(ipAddress);
                var swipeList = swipes.ReturnAsSwipeList(ipAddress);

                using (var dbContext = new DataContext())
                {
                    var swipeService = new SwipeService(dbContext);
                    foreach (var swipe in swipeList)
                    {
                        await swipeService.AddSwipe(swipe.ReturnAsSwipe());
                    }
                }
            }
            finally
            {
                operations.Find(x => x.IpAddress == ipAddress).CurrentStatus = Operation.Status.Finished;
                _pool.Release();
            }
        }
    }
}
