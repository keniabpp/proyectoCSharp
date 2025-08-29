

namespace Application.Features.Tareas.DTOs
{
    public class TareaDTO
    {

        public int id_tarea { get; set; }
        public required string titulo { get; set; }
        public required string descripcion { get; set; }
        public required string detalle { get; set; }
        public DateTime fecha_vencimiento { get; set; } = DateTime.Now;
        public int creado_por { get; set; }
        public string nombre_creador { get; set; } = string.Empty;
        public int asignado_a { get; set; }
        public string nombre_asignado { get; set; } = string.Empty;
        public required string nombre_columna { get; set; }

        public required string estado_fechaVencimiento { get; set; }

    }
}