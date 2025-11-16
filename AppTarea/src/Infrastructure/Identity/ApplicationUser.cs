using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace AppTarea.Infrastructure.Identity
{
    /// <summary>
    /// Entidad Identity que extiende IdentityUser para trabajar con ASP.NET Core Identity
    /// Se ubica en Infrastructure para no contaminar el Domain con dependencias externas
    /// </summary>
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        // Relación con el rol personalizado (manteniendo compatibilidad)
        [ForeignKey("ApplicationRole")]
        [Column("id_rol")]
        public int? CustomRoleId { get; set; }
        public ApplicationRole? CustomRole { get; set; }

        // Relaciones con tableros (manteniendo la funcionalidad existente)
        public ICollection<Tablero> Tableros { get; set; } = new List<Tablero>();

        // Nota: Las relaciones con Tarea se mantienen en la entidad Usuario original
        // para preservar la integridad del modelo de dominio existente
        // ApplicationUser es principalmente para funciones de Identity

        public ApplicationUser()
        {
            // Configuración por defecto para Identity
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public ApplicationUser(string nombre, string apellido, string telefono, string email)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Email = email;
            this.UserName = email; // Usamos email como username
            this.SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}