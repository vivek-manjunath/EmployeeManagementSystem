using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementSystem.Core.Models;

namespace EmployeeManagementSystem.Application.Interfaces.Persistence
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetAllWithDepartment();
        Task<Employee> GetWithDepartment(int id);
    }
}