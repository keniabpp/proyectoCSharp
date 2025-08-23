using Application.Features.Usuarios.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Usuarios.Queries
{
    public class GetUsuarioByIdQuery : IRequest<UsuarioDTO>
    {
        public int Id { get; set; }

        public GetUsuarioByIdQuery(int id)
        {
            Id = id;
        }
    }
}
