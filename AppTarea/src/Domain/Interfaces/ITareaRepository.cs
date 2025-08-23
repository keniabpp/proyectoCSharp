using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITareaRepository
    {
        Task<IEnumerable<Tarea>> GetAllAsync();
        Task<Tarea> GetByIdAsync(int id);
        Task<Tarea> CreateAsync(Tarea tarea);
        Task<Tarea> UpdateAsync(int id, Tarea tarea);
        Task<bool> DeleteAsync(int id);
        Task<bool> MoverTareaAsync(int id_tarea, int id_columna);

        
    }
}