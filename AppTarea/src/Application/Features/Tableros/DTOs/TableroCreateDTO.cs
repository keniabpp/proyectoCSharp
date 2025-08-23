namespace Application.Features.Tableros.DTOs
{
    public class TableroCreateDTO
    {
        public string nombre { get; set; } = null!;
        public int creado_por { get; set; } 
        public int id_rol { get; set; }
    }
}
