using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTarea.Infrastructure.Identity
{
    /// <summary>
    /// Entidad Identity que extiende IdentityRole para trabajar con ASP.NET Core Identity
    /// Se ubica en Infrastructure para mantener el Domain limpio de dependencias externas
    /// </summary>
    [Table("ApplicationRoles")]
    public class ApplicationRole : IdentityRole<int>
    {
        [Required]
        [MaxLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        // Relación con usuarios que usan este rol personalizado
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public ApplicationRole()
        {
            // Constructor por defecto
        }

        public ApplicationRole(string nombre) : base(nombre)
        {
            this.Nombre = nombre;
            this.Name = nombre; // Para compatibilidad con Identity
        }
    }
}