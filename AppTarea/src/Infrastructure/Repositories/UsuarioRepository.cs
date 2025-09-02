using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppTarea.Infrastructure.Persistence.Context;

namespace AppTarea.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.id_usuario == id);

        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            await _context.Entry(usuario).Reference(u => u.Rol).LoadAsync();

            return usuario;
        }

        public async Task<Usuario> UpdateAsync(int id, Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            await _context.SaveChangesAsync();
            await _context.Entry(usuarioExistente!).Reference(u => u.Rol).LoadAsync();
            return usuarioExistente!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.email == email);
        }
        
        

    }
}
