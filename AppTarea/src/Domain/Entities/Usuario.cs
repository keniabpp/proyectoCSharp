using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("apellido")]
        public string apellido { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("telefono")]
        public string telefono { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("contrasena")]
        public string contrasena { get; set; } = string.Empty;
        [JsonIgnore]

        // id_rol referencia AspNetRoles por ID (sin navegación para mantener Domain limpio)
        [Column("id_rol")]
        public int id_rol { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string apellido, string telefono, string email, string contrasena, int id_rol)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.email = email;
            this.contrasena = contrasena;
            this.id_rol = id_rol;
        }

        
    }
}


