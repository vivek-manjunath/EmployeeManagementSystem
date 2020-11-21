using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetAsync(int id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}