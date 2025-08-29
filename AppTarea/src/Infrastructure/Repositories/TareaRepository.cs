using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppTarea.Infrastructure.Persistence.Context;

namespace AppTarea.Infrastructure.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly AppDbContext _context;

        public TareaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarea> CreateAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return false;

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync()
        {
            return await _context.Tareas.Include(t => t.creador).Include(t => t.asignado).Include(t => t.tablero)
            .Include(t => t.columna).ToListAsync();
        }

        public async Task<Tarea?> GetByIdAsync(int id)
        {
            return await _context.Tareas.Include(t => t.creador).Include(t => t.asignado)
            .Include(t => t.tablero).Include(t => t.columna).FirstOrDefaultAsync(t => t.id_tarea == id);

            
        }

        public async Task<bool> MoverTareaAsync(int id_tarea, int id_columna)
        {
            var tarea = await _context.Tareas.FindAsync(id_tarea);
            if (tarea == null) return false;

            tarea.id_columna = id_columna;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Tarea>> TareasAsignadasAsync(int id_usuario)
        {
            return await _context.Tareas.Include(t => t.creador).Include(t => t.asignado)
            .Include(t => t.columna).Where(t => t.asignado_a == id_usuario).ToListAsync();
        }

        public async Task<Tarea> UpdateAsync(int id, Tarea tarea)
        {
            var tareaExistente = await _context.Tareas.FindAsync(id);
            if (tareaExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró la tarea con ID {id}");

            }

            tareaExistente.titulo = tarea.titulo;
            tareaExistente.descripcion = tarea.descripcion;
            
            
            await _context.SaveChangesAsync();
            await _context.Entry(tareaExistente).Reference(t => t.creador).LoadAsync();
            await _context.Entry(tareaExistente).Reference(t => t.asignado).LoadAsync();

            return tareaExistente;
        }
    }
}
