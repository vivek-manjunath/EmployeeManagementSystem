using System.Collections.Generic;
using EmployeeManagementSystem.Core.Models;

namespace EmployeeManagementSystem.Application.Interfaces.Persistence
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<Department> GetAllDepartmentsWithEmployees();
    }
}