using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Models;

namespace EmployeeManagementSystem.Services.Interfaces
{
    public interface IEmployeeService : IEntityService<Employee, EmployeeDto>
    {

    }
}