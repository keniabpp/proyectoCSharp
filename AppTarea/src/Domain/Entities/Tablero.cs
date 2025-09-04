using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{

    [Table("Tableros")]

    public class Tablero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_tablero { get; set; }

        public string nombre { get; set; } = string.Empty;
        public DateTime creado_en { get; set; } = DateTime.Now;

        [ForeignKey("Usuario")]
        public int creado_por { get; set; } // Clave foránea
        public Usuario usuario { get; set; } = null!;
        
        [ForeignKey("Rol")]
        [Column("id_rol")]
        public int id_rol { get; set; }

        public Rol rol { get; set; } = null!;

        [InverseProperty("tablero")]
        public ICollection<Tarea> tareas { get; set; } = new List<Tarea>();


        public Tablero()
        {

        }

        public Tablero(string nombre, DateTime creado_en, int creado_por, int id_rol)
        {
            this.nombre = nombre;
            this.creado_en = creado_en;
            this.creado_por = creado_por;
            this.id_rol = id_rol;
         
        
        }
    }
    
}


    


       


    

    


    

    