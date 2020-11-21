using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Interfaces
{
    public interface IEntityService<Entity, DTO>
    {
         
         IEnumerable<DTO> Get();
         Task<DTO> Get(int id);
         void Create(Entity entity);
         void Update(Entity entity);
         void Delete(int id);
         IEnumerable<DTO> SearchByName(string name);
    }
}