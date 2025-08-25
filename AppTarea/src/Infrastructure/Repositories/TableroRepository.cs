using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppTarea.Infrastructure.Persistence.Context;

namespace AppTarea.Infrastructure.Repositories
{
    public class TableroRepository : ITableroRepository
    {
        private readonly AppDbContext _context;

        public TableroRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Tablero> CreateAsync(Tablero tablero)
        {
            _context.Tableros.Add(tablero);
            await _context.SaveChangesAsync();

            await _context.Entry(tablero).Reference(t => t.usuario).LoadAsync();
            await _context.Entry(tablero).Reference(t => t.rol).LoadAsync();

           return tablero;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tablero = await _context.Tableros.FindAsync(id);
            if (tablero == null) return false;
            _context.Tableros.Remove(tablero);
            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<IEnumerable<Tablero>> GetAllAsync()
        {
            return await _context.Tableros.Include(t => t.usuario).ToListAsync();
        }

        public async Task<Tablero?> GetByIdAsync(int id)
        {
            return await _context.Tableros.Include(t => t.usuario)
           .FirstOrDefaultAsync(t => t.id_tablero == id);

        }

        public async Task<Tablero> UpdateAsync(int id, Tablero tablero)
        {
            var tableroExistente = await _context.Tableros.FindAsync(id);
            if (tableroExistente == null)
            {
                throw new Exception($"No se encontró el Tablero con ID {id}");
            }

            tableroExistente.nombre = tablero.nombre;
            
            await _context.SaveChangesAsync();
            await _context.Entry(tableroExistente).Reference(t => t.usuario).LoadAsync();

            return tableroExistente;
        }
    }
}