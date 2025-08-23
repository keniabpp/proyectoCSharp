using Application.Features.Tableros.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tableros.Commands
{
    public class UpdateTableroCommand : IRequest<TableroDTO>
    {
        public int Id { get; set; }

        public TableroUpdateDTO TableroUpdateDTO { get; set; }

        public UpdateTableroCommand(int id, TableroUpdateDTO tableroUpdateDTO)
        {
            Id = id;
            TableroUpdateDTO = tableroUpdateDTO;
        }
    }
}