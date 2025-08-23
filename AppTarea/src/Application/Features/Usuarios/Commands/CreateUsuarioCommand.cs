using Application.Features.Usuarios.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Commands
{
    public class CreateUsuarioCommand : IRequest<UsuarioDTO>
    {
        public UsuarioCreateDTO UsuarioCreateDTO { get; set; }

        public CreateUsuarioCommand(UsuarioCreateDTO usuarioCreateDTO)
        {
            UsuarioCreateDTO = usuarioCreateDTO;
        }
    }
}
