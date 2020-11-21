using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Application.Interfaces.Persistence;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext dbContext;
        public EmployeeRepository(AppDbContext _dbContext) : base(_dbContext) => this.dbContext = _dbContext;

        public IEnumerable<Employee> GetAllWithDepartment()
        {
            return dbContext.Employees.Include(e => e.Department).ToList();
        }
        public async Task<Employee> GetWithDepartment(int id)
        {
            return await dbContext.Employees.Where(e => e.Id == id)
                .Include(e => e.Department).FirstOrDefaultAsync();
        }
    }
}