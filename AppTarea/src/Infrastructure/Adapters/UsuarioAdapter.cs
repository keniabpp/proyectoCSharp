using Domain.Entities;
using AppTarea.Infrastructure.Identity;

namespace AppTarea.Infrastructure.Adapters
{
    /// <summary>
    /// Adaptador para mantener compatibilidad entre ApplicationUser (Identity) y Usuario (entidad de Domain)
    /// Se ubica en Infrastructure porque depende de las entidades de Identity
    /// </summary>
    public static class UsuarioAdapter
    {
        /// <summary>
        /// Convierte un ApplicationUser a Usuario para mantener compatibilidad con el código existente
        /// </summary>
        public static Usuario ToUsuario(this ApplicationUser applicationUser)
        {
            if (applicationUser == null) 
                throw new ArgumentNullException(nameof(applicationUser));

            return new Usuario
            {
                id_usuario = applicationUser.Id,
                nombre = applicationUser.Nombre,
                apellido = applicationUser.Apellido,
                telefono = applicationUser.Telefono,
                email = applicationUser.Email ?? string.Empty,
                contrasena = string.Empty, // Por seguridad, no exponemos el hash
                id_rol = applicationUser.CustomRoleId ?? 0
                // Las relaciones con Tarea se cargan por separado cuando sea necesario
            };
        }

        /// <summary>
        /// Convierte un Usuario a ApplicationUser para operaciones de Identity
        /// </summary>
        public static ApplicationUser ToApplicationUser(this Usuario usuario)
        {
            if (usuario == null) 
                throw new ArgumentNullException(nameof(usuario));

            return new ApplicationUser
            {
                Id = usuario.id_usuario,
                Nombre = usuario.nombre,
                Apellido = usuario.apellido,
                Telefono = usuario.telefono,
                Email = usuario.email,
                UserName = usuario.email,
                CustomRoleId = usuario.id_rol > 0 ? usuario.id_rol : null
                // Las relaciones con Tarea permanecen en la entidad Usuario original
            };
        }

        /// <summary>
        /// Actualiza un ApplicationUser con datos de un Usuario
        /// </summary>
        public static void UpdateFromUsuario(this ApplicationUser applicationUser, Usuario usuario)
        {
            if (applicationUser == null) 
                throw new ArgumentNullException(nameof(applicationUser));
            if (usuario == null) 
                throw new ArgumentNullException(nameof(usuario));

            applicationUser.Nombre = usuario.nombre;
            applicationUser.Apellido = usuario.apellido;
            applicationUser.Telefono = usuario.telefono;
            applicationUser.Email = usuario.email;
            applicationUser.UserName = usuario.email;
            applicationUser.CustomRoleId = usuario.id_rol > 0 ? usuario.id_rol : null;
        }
    }
}