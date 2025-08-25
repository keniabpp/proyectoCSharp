using Application.Features.Usuarios.DTOs;
using MediatR;

namespace Application.Features.Usuarios.Commands
{
    public class UpdateUsuarioCommand : IRequest<UsuarioDTO>
    {
        public int Id { get; set; }
        public UsuarioUpdateDTO UsuarioUpdateDTO { get; set; }

        public UpdateUsuarioCommand(int id, UsuarioUpdateDTO usuarioUpdateDTO)
        {
            Id = id;
            UsuarioUpdateDTO = usuarioUpdateDTO;
        }
    }
}
