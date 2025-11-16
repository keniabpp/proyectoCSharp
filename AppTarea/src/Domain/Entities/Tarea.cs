using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Tareas")]
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_tarea")]
        public int id_tarea { get; set; }

        public string titulo { get; set; } = string.Empty;

        public string descripcion { get; set; } = string.Empty;

        public String detalle { get; set; } = string.Empty;

        public DateTime creado_en { get; set; } = DateTime.Now;

        public DateTime fecha_vencimiento { get; set; } = DateTime.Now;

        public int creado_por { get; set; }

        public int asignado_a { get; set; }
        
        public int id_tablero { get; set; }

        public int id_columna { get; set; }

        // Propiedades de navegación solo a entidades del Domain
        // creado_por y asignado_a referencian AspNetUsers por ID (sin navegación para mantener Domain limpio)
        
        [ForeignKey("id_tablero")]
        public Tablero tablero { get; set; } = null!;

        [ForeignKey("id_columna")]
        public Columna columna { get; set; } = null!;

        public Tarea() { }

        public Tarea(string titulo, string descripcion, string detalle, DateTime creado_en, DateTime fecha_vencimiento, int creado_por, int asignado_a, int id_tablero, int id_columna)
        {
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.detalle = detalle;
            this.creado_en = creado_en;
            this.fecha_vencimiento = fecha_vencimiento;
            this.creado_por = creado_por;
            this.asignado_a = asignado_a;
            this.id_tablero = id_tablero;
            this.id_columna = id_columna;
        }
    }
}
