using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Application.Interfaces.Persistence;
using EmployeeManagementSystem.Persistence.DBContext;

namespace EmployeeManagementSystem.Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        private readonly AppDbContext dBContext;
        public Repository (AppDbContext dBContext) {
            this.dBContext = dBContext;
        }
        public void Add (TEntity entity) {
            dBContext.Set<TEntity> ().Add (entity);
        }

        public void AddRange (IEnumerable<TEntity> entities) {
            throw new System.NotImplementedException ();
        }

        public async Task<TEntity> GetAsync (int id) {
            return await dBContext.Set<TEntity> ().FindAsync (id);
        }

        public IEnumerable<TEntity> GetAll () {
            return dBContext.Set<TEntity> ().ToList ();
        }

        public void Remove (TEntity entity) {
            dBContext.Set<TEntity> ().Remove (entity);
        }

        public void Update (TEntity entity) {
            dBContext.Set<TEntity> ().Update (entity);
        }
    }
}