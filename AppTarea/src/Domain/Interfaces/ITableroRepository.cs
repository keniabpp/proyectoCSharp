using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITableroRepository
    {
        Task<IEnumerable<Tablero>> GetAllAsync();
        Task<Tablero> GetByIdAsync(int id);
        Task<Tablero> CreateAsync(Tablero tablero);
        Task<Tablero> UpdateAsync(int id, Tablero tablero);
        Task<bool> DeleteAsync(int id);
        
    }
}