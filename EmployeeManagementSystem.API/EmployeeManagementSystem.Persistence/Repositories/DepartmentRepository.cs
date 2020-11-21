using System.Collections.Generic;
using EmployeeManagementSystem.Persistence.DBContext;
using EmployeeManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Application.Interfaces.Persistence;

namespace EmployeeManagementSystem.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext dbContext;
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext) => this.dbContext = dbContext;

        public IEnumerable<Department> GetAllDepartmentsWithEmployees()
        {
            return dbContext.Departments
                .Include(department => department.Employees);
        }
    }
}