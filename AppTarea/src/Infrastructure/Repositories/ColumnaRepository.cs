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

       

        
        public async Task<IEnumerable<Columna>> GetAllAsync()
        {
            return await _context.Columnas.ToListAsync();
            
        }

        public async Task<Columna?> GetByIdAsync(int id)
        {
            return await _context.Columnas.FirstOrDefaultAsync(c => c.id_columna == id);
            
            
        }

       
    }
}