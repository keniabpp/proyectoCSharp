using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    [Table("Columnas")]

    public class Columna
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id_columna { get; set; }

        public string nombre { get; set; } = string.Empty;

        public EstadoColumna posicion { get; set; }

        [ForeignKey("Tablero")]
        [Column("id_tablero")]
        public int id_tablero { get; set; }
        public Tablero tablero { get; set; } = null!;

        [InverseProperty("columna")]
        public ICollection<Tarea> tareas { get; set; } = new List<Tarea>();

        public Columna() { }

        public Columna(string nombre, EstadoColumna posicion, int id_tablero)
        {
            this.nombre = nombre;
            this.posicion = posicion;
            this.id_tablero = id_tablero;
        }
        
    }
}