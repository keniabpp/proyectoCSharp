using Application.Features.Usuarios.DTOs;
using MediatR;

namespace Application.Features.Usuarios.Queries
{
    public class GetAllUsuariosQuery : IRequest<IEnumerable<UsuarioDTO>>
    {
    }
}
