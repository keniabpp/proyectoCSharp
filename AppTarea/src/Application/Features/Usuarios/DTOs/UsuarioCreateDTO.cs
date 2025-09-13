namespace Application.Features.Usuarios.DTOs
{
    public class UsuarioCreateDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int id_rol { get; set; }
    }
}