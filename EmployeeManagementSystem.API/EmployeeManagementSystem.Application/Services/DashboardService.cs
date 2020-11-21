using System.Linq;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Application.Interfaces.Persistence;

namespace EmployeeManagementSystem.Application.Services
{
    public class DashboardService
    {
        private readonly IUnitOfWork unitOfWork;
        public DashboardService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;

        }
        public DashboardInfo Get()
        {
            DashboardInfo dashboardInfo = new DashboardInfo();

            dashboardInfo.EmployeeCount = unitOfWork.employeeRepository.GetAll().Count();
            dashboardInfo.DepartmentCount = unitOfWork.departmentRepository.GetAll().Count();
            return dashboardInfo;
        }
    }
}