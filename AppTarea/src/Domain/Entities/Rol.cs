using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_rol")] // Nombre exacto en la base de datos
        public int id_rol { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nombre")]
        public string nombre { get; set; } = string.Empty;

        public Rol() { }

        public Rol(string nombre)
        {
            this.nombre = nombre;
        }

        public override string ToString()
        {
            return $"Rol: {id_rol} - {nombre}";
        }
    }
}
