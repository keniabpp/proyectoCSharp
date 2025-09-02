using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITareaRepository : IGenericRepository<Tarea, int>
    {
        
        Task<bool> MoverTareaAsync(int id_tarea, int id_columna);
        Task<List<Tarea>> TareasAsignadasAsync(int id_usuario);

        
    }
}