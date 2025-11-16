using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    /// <summary>
    /// Interfaz que extiende las capacidades del repositorio de usuarios para trabajar con Identity
    /// Se ubica en Application porque define contratos de casos de uso, no implementación
    /// Mantiene la compatibilidad con IUsuarioRepository del Domain
    /// </summary>
    public interface IApplicationUserService : IUsuarioRepository
    {
        // Métodos específicos de Identity que van más allá del repository pattern básico
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(int userId);
        Task<bool> ConfirmEmailAsync(int userId, string token);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
        Task<IList<string>> GetRolesAsync(int userId);
        Task AddToRoleAsync(int userId, string role);
        Task RemoveFromRoleAsync(int userId, string role);
        Task<Usuario> CreateWithPasswordAsync(Usuario usuario, string password);
        Task<(bool IsSuccess, Usuario User, IEnumerable<string> Errors)> CreateUserAsync(string email, string password, string nombre, string apellido, string telefono, string role);
        Task<(bool IsSuccess, Usuario User, IEnumerable<string> Errors)> CreateUserWithRoleIdAsync(string email, string password, string nombre, string apellido, string telefono, int roleId);
        Task<string?> GetUserNameByIdAsync(int userId);
        Task<(bool IsSuccess, bool IsLockedOut, string? ErrorMessage)> ValidatePasswordAsync(string email, string password);
    }
}