using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario, int>
    {
        Task<Usuario?> GetByEmailAsync(string email);
        
    }
}
