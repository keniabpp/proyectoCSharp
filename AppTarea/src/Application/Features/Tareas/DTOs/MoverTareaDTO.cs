

namespace Application.Features.Tareas.DTOs
{
    public class MoverTareaDTO
    {
        public int id_tarea { get; set; }
        public int id_columna { get; set; }
        public required string detalle { get; set; }
    }

}
