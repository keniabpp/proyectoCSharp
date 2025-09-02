using Domain.Entities;
using System.Collections.Generic;   
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T, TId>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(TId id, T entity);
        Task<bool> DeleteAsync(TId id);
    }
}