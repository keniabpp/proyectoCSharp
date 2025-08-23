
using Application.Features.Tableros.DTOs;
using MediatR;

namespace Application.Features.Tableros.Queries
{
    public class GetTableroByIdQuery : IRequest<TableroDTO>
    {
        public int Id { get; set; }
        public GetTableroByIdQuery(int id)
        {
            Id = id;
        }
    }
}