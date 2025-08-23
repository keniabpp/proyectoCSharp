using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppTarea.Infrastructure.Persistence.Context;

namespace AppTarea.Infrastructure.Repositories
{
    public class ColumnaRepository : IColumnaRepository
    {
        private readonly AppDbContext _context;

        public ColumnaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Columna> CreateAsync(Columna columna)
        {
            await _context.Columnas.AddAsync(columna);
            await _context.SaveChangesAsync();
            return columna;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var columna = await _context.Columnas.FindAsync(id);
            //var columna = await _context.Columnas.FirstOrDefaultAsync(c => c.id_columna == id);
            if (columna == null) return false;
            _context.Columnas.Remove(columna);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Columna>> GetAllAsync()
        {
            return await _context.Columnas.ToListAsync();
            //return await _context.Columnas.Include(c => c.id_tablero).ToListAsync();
        }

        public async Task<Columna> GetByIdAsync(int id)
        {
            var columna = await _context.Columnas.FirstOrDefaultAsync(c => c.id_columna == id);
            if (columna == null)
            {
                throw new KeyNotFoundException($"Columna con ID {id} no encontrada.");
            }


            return columna;
            
        }

       
    }
}