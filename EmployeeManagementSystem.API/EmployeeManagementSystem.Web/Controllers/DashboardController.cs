using System.Net.Mime;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Web.Controllers
{
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService dashboardService;
        public DashboardController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;

        }
        [HttpGet]
        public ActionResult<DashboardInfo> Get()
        {
            return Ok(dashboardService.Get());
        }
    }
}