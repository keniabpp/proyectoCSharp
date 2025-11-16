namespace Application.Features.Usuarios.DTOs
{
    public class UsuarioRegisterResponseDTO
    {
        public required string id_usuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int id_rol { get; set; }
        public string? rolNombre { get; set; }
    }
}