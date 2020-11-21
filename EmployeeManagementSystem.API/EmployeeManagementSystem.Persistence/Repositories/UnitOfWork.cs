using System.Threading.Tasks;
using EmployeeManagementSystem.Application.Interfaces.Persistence;
using EmployeeManagementSystem.Persistence.DBContext;

namespace EmployeeManagementSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            employeeRepository = new EmployeeRepository(dbContext);
            departmentRepository = new DepartmentRepository(dbContext);
        }
        public IEmployeeRepository employeeRepository { get; private set; }
        public IDepartmentRepository departmentRepository { get; private set; }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}