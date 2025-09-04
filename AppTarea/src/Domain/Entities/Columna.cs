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

        

        [InverseProperty("columna")]
        public ICollection<Tarea> tareas { get; set; } = new List<Tarea>();

        public Columna() { }

        public Columna(string nombre, EstadoColumna posicion)
        {
            this.nombre = nombre;
            this.posicion = posicion;
            
        }
        
    }
}