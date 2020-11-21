using System.Threading.Tasks;
using EmployeeManagementSystem.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagementSystem.Hubs
{
    public class DashboardHub : Hub
    {
        private readonly IHubContext<DashboardHub> hubContext;

        public DashboardHub(IHubContext<DashboardHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task RefreshDashboard(DashboardInfo dashboardInfo)
        {
            await this.hubContext.Clients.All.SendAsync("RefreshData", dashboardInfo);
        }

        public async Task DepartmentUpdated(string message)
        {
            await this.hubContext.Clients.All.SendAsync("DepartmentUpdated", message);
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Message", "Connected to hub successfully!");
        }
    }
}