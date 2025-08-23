namespace Application.Features.Usuarios.DTOs
{
    public class UsuarioCreateDTO
    {
        public string nombre { get; set; } = null!;
        public string apellido { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public string email { get; set; } = null!;
        public string contrasena { get; set; } = null!;
        public int id_rol { get; set; }
    }
}
