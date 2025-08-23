using Application.Features.Tableros.DTOs;
using MediatR;

namespace Application.Features.Tableros.Commands
{
    public class CreateTableroCommand : IRequest<TableroDTO>
    {
        public TableroCreateDTO TableroCreateDTO { get; set; }

        public CreateTableroCommand(TableroCreateDTO tableroCreateDTO)
        {
            TableroCreateDTO = tableroCreateDTO;
        }
    }
    
}