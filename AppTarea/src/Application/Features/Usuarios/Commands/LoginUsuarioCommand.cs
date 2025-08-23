using Application.Features.Usuarios.DTOs;
using MediatR;

namespace Application.Features.Usuarios.Commands
{
    public class LoginUsuarioCommand : IRequest<UsuarioLoginResponseDTO?>
    {
        public UsuarioLoginDTO UsuarioLoginDTO { get; set; }

        public LoginUsuarioCommand(UsuarioLoginDTO usuarioLoginDTO)
        {
            UsuarioLoginDTO = usuarioLoginDTO;
        }
    }
}
