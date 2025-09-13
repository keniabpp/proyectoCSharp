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

        public ICollection<Tablero> Tableros { get; set; } = new List<Tablero>();

        [ForeignKey("Rol")]
        [Column("id_rol")]
        public int id_rol { get; set; }

        public Rol? Rol { get; set; }

        // Relaciones con tareas
        [InverseProperty("creador")]
        public ICollection<Tarea> tareas_creadas { get; set; } = new List<Tarea>();

        [InverseProperty("asignado")]
        public ICollection<Tarea> tareas_asignadas { get; set; } = new List<Tarea>();

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

        public override string ToString()
        {
            return $"Usuario: {id_usuario} - {nombre} {apellido} ({email}) - Rol ID: {id_rol}";
        }
    }
}


