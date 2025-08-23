namespace Application.Features.Usuarios.DTOs
{
    public class UsuarioDTO
    {
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        public int id_rol { get; set; }
        public string? rolNombre { get; set; }
    }
}