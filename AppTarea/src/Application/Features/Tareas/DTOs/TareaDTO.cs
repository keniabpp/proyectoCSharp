

namespace Application.Features.Tareas.DTOs
{
    public class TareaDTO
    {

        public int id_tarea { get; set; } 
        public string titulo { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public  string detalle { get; set; } = string.Empty;
        public DateTime fecha_vencimiento { get; set; } = DateTime.Now;
        public int creado_por { get; set; } 
        public string nombre_creador { get; set; } = string.Empty;
        public int asignado_a { get; set; }
        public string nombre_asignado { get; set; } = string.Empty;
        public string nombre_columna { get; set; } = string.Empty;

        public  string estado_fechaVencimiento { get; set; } = string.Empty;

    }
}