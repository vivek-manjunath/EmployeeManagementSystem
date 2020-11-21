using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository employeeRepository { get; }
        IDepartmentRepository departmentRepository { get; }
        Task<int> CompleteAsync();
    }
}