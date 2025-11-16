using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces;
using AppTarea.Infrastructure.Identity;
using AppTarea.Infrastructure.Adapters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppTarea.Infrastructure.Persistence.Context;

namespace AppTarea.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación que combina ASP.NET Core Identity con el patrón repository existente
    /// Mantiene compatibilidad con la interfaz IUsuarioRepository mientras agrega capacidades de Identity
    /// Se ubica en Infrastructure porque es detalle de implementación
    /// </summary>
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserService(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Implementación de IUsuarioRepository (compatibilidad hacia atrás)

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var applicationUsers = await _userManager.Users
                .Include(u => u.CustomRole)
                .Include(u => u.Tableros)
                .ToListAsync();

            return applicationUsers.Select(au => au.ToUsuario());
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            var applicationUser = await GetApplicationUserByIdAsync(id);
            return applicationUser?.ToUsuario();
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var applicationUser = usuario.ToApplicationUser();
            
            // Si no se proporciona contraseña, usar una temporal
            var password = !string.IsNullOrEmpty(usuario.contrasena) ? usuario.contrasena : "TempPass123!";
            
            var result = await CreateApplicationUserAsync(applicationUser, password);
            return result.ToUsuario();
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            var applicationUser = await GetApplicationUserByIdAsync(usuario.id_usuario);
            if (applicationUser == null)
                throw new InvalidOperationException($"Usuario with ID {usuario.id_usuario} not found");

            applicationUser.UpdateFromUsuario(usuario);
            var result = await UpdateApplicationUserAsync(applicationUser);
            return result.ToUsuario();
        }

        public async Task<Usuario> UpdateAsync(int id, Usuario usuario)
        {
            usuario.id_usuario = id; // Asegurar que el ID coincida
            return await UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await DeleteApplicationUserAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            var applicationUser = await GetApplicationUserByEmailAsync(email);
            return applicationUser?.ToUsuario();
        }

        #endregion

        #region Métodos específicos de Identity

        private async Task<ApplicationUser?> GetApplicationUserByEmailAsync(string email)
        {
            return await _userManager.Users
                .Include(u => u.CustomRole)
                .Include(u => u.Tableros)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        private async Task<ApplicationUser?> GetApplicationUserByIdAsync(int id)
        {
            return await _userManager.Users
                .Include(u => u.CustomRole)
                .Include(u => u.Tableros)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        private async Task<ApplicationUser> CreateApplicationUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user: {errors}");
            }

            return user;
        }

        private async Task<ApplicationUser> UpdateApplicationUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to update user: {errors}");
            }

            return user;
        }

        private async Task DeleteApplicationUserAsync(int id)
        {
            var user = await GetApplicationUserByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to delete user: {errors}");
                }
            }
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await GetApplicationUserByEmailAsync(email);
            if (user == null) return false;
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> ConfirmEmailAsync(int userId, string token)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            if (user == null) return false;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await GetApplicationUserByEmailAsync(email);
            if (user == null) throw new InvalidOperationException("User not found");
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await GetApplicationUserByEmailAsync(email);
            if (user == null) return false;
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetRolesAsync(int userId)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            if (user == null) return new List<string>();
            return await _userManager.GetRolesAsync(user);
        }

        public async Task AddToRoleAsync(int userId, string role)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");
            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to add user to role: {errors}");
            }
        }

        public async Task RemoveFromRoleAsync(int userId, string role)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to remove user from role: {errors}");
            }
        }

        public async Task<Usuario> CreateWithPasswordAsync(Usuario usuario, string password)
        {
            var applicationUser = usuario.ToApplicationUser();
            var result = await CreateApplicationUserAsync(applicationUser, password);
            return result.ToUsuario();
        }

        public async Task<(bool IsSuccess, Usuario User, IEnumerable<string> Errors)> CreateUserAsync(string email, string password, string nombre, string apellido, string telefono, string role)
        {
            try
            {
                // Buscar el rol por nombre para obtener su ID
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity == null)
                {
                    return (false, null!, new[] { $"Rol '{role}' no encontrado" });
                }

                var applicationUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = telefono,
                    EmailConfirmed = true,
                    CustomRoleId = roleEntity.Id // Establecer el id_rol para mantener compatibilidad
                };

                var result = await _userManager.CreateAsync(applicationUser, password);
                
                if (result.Succeeded)
                {
                    // Asignar rol al usuario en AspNetUserRoles
                    var addToRoleResult = await _userManager.AddToRoleAsync(applicationUser, role);
                    
                    if (!addToRoleResult.Succeeded)
                    {
                        // Si falla, eliminar el usuario y devolver error
                        await _userManager.DeleteAsync(applicationUser);
                        var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                        return (false, null!, new[] { $"Error al asignar rol: {errors}" });
                    }
                    
                    // Cargar datos completos del usuario creado
                    var createdUser = await GetApplicationUserByEmailAsync(email);
                    
                    return (true, createdUser!.ToUsuario(), Enumerable.Empty<string>());
                }

                return (false, null!, result.Errors.Select(e => e.Description));
            }
            catch (Exception ex)
            {
                return (false, null!, new[] { ex.Message });
            }
        }

        public async Task<(bool IsSuccess, Usuario User, IEnumerable<string> Errors)> CreateUserWithRoleIdAsync(string email, string password, string nombre, string apellido, string telefono, int roleId)
        {
            try
            {
                // Buscar el rol por ID
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                if (role == null)
                {
                    return (false, null!, new[] { $"Rol con ID {roleId} no encontrado" });
                }

                var applicationUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = telefono,
                    EmailConfirmed = true,
                    CustomRoleId = roleId // Establecer el id_rol para mantener compatibilidad
                };

                var result = await _userManager.CreateAsync(applicationUser, password);
                
                if (result.Succeeded)
                {
                    // Asignar rol al usuario usando el nombre del rol en AspNetUserRoles
                    var addToRoleResult = await _userManager.AddToRoleAsync(applicationUser, role.Name!);
                    
                    if (!addToRoleResult.Succeeded)
                    {
                        // Si falla, eliminar el usuario y devolver error
                        await _userManager.DeleteAsync(applicationUser);
                        var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                        return (false, null!, new[] { $"Error al asignar rol: {errors}" });
                    }
                    
                    // Cargar datos completos del usuario creado
                    var createdUser = await GetApplicationUserByEmailAsync(email);
                    
                    return (true, createdUser!.ToUsuario(), Enumerable.Empty<string>());
                }

                return (false, null!, result.Errors.Select(e => e.Description));
            }
            catch (Exception ex)
            {
                return (false, null!, new[] { ex.Message });
            }
        }

        public async Task<string?> GetUserNameByIdAsync(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.Nombre;
        }

        public async Task<(bool IsSuccess, bool IsLockedOut, string? ErrorMessage)> ValidatePasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, false, "Usuario no encontrado");
            }

            // Verificar si la cuenta está bloqueada
            if (await _userManager.IsLockedOutAsync(user))
            {
                return (false, true, "Cuenta bloqueada por múltiples intentos fallidos");
            }

            // Verificar la contraseña
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            
            if (isPasswordValid)
            {
                // Si la contraseña es correcta, resetear el contador de fallos
                await _userManager.ResetAccessFailedCountAsync(user);
                return (true, false, null);
            }
            else
            {
                // Si la contraseña es incorrecta, incrementar el contador de fallos
                await _userManager.AccessFailedAsync(user);
                
                // Verificar si ahora está bloqueado
                if (await _userManager.IsLockedOutAsync(user))
                {
                    return (false, true, "Cuenta bloqueada por múltiples intentos fallidos");
                }
                
                return (false, false, "Contraseña incorrecta");
            }
        }

        #endregion
    }
}