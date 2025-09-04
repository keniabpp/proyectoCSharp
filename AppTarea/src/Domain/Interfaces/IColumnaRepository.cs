using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IColumnaRepository
    {
        Task<IEnumerable<Columna>> GetAllAsync();
        Task<Columna?> GetByIdAsync(int id);
       
        
        
    }
}
