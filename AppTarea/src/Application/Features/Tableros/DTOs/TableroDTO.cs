namespace Application.Features.Tableros.DTOs
{
    public class TableroDTO
    {
        public int id_tablero { get; set; }
        public required string nombre { get; set; }
       
        public int creado_por { get; set; }
        public string? nombre_usuario { get; set; }  
        
    }
}
