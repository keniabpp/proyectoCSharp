using Application.Features.Usuarios.DTOs;
using MediatR;

namespace Application.Features.Usuarios.Commands
{
    public record RegisterUsuarioCommand(UsuarioRegisterDTO UsuarioRegisterDTO) : IRequest<UsuarioDTO>;
}
