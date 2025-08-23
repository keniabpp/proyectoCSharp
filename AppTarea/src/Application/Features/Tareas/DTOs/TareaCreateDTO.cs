namespace Application.Features.Tareas.DTOs
{
    public class TareaCreateDTO
    {
        public string titulo { get; set; } = null!;
        public string descripcion { get; set; } = null!;

        public DateTime fecha_vencimiento { get; set; } = DateTime.Now;

        public int creado_por { get; set; }

        public int asignado_a { get; set; }
        
        public int id_tablero { get; set; }
        public int id_columna { get; set; }
    }
}