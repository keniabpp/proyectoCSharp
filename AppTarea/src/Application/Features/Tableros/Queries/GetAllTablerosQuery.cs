using Application.Features.Tableros.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tableros.Queries
{
    public class GetAllTablerosQuery : IRequest<IEnumerable<TableroDTO>>
    {

    }
}