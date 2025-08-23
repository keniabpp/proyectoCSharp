namespace Application.Features.Columnas.DTOs
{
    public class ColumnaCreateDTO
    {

        public required string nombre { get; set; }
        public int posicion { get; set; }
        public int id_tablero { get; set; }  
        
    }
}
