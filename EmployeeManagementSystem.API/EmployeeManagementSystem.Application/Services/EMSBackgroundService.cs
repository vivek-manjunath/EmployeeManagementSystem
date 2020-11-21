using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagementSystem.Hubs;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.Services
{
    public class EMSBackgroundService : BackgroundService
    {
        private readonly IHubContext<DashboardHub> hubContext;
        private readonly ILogger<EMSBackgroundService> logger;
        private readonly IDepartmentService departmentService;

        public EMSBackgroundService(IHubContext<DashboardHub> hubContext, ILogger<EMSBackgroundService> logger, IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
            this.logger = logger;
            this.hubContext = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // logger.LogInformation($"Current time: {DateTime.Now.ToString()}");
                using (var hub = new DashboardHub(hubContext))
                {
                    await hub.DepartmentUpdated($"Current time: {DateTime.Now.ToString()}");
                }
                // await Task.Delay(10000);
            }
            // return Task.CompletedTask;
        }
    }
}