using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.Repository.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        Task<T> AddAsync(T entity);

        Task<T> DeleteAsync(Guid id);

        Task SaveChangesAsync();
    }
}
