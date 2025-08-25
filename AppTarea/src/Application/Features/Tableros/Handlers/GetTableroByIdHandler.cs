using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Tableros.Handlers
{
    public class GetTableroByIdHandler : IRequestHandler<GetTableroByIdQuery, TableroDTO>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IMapper _mapper;

        public GetTableroByIdHandler(ITableroRepository tableroRepository, IMapper mapper)
        {
            _tableroRepository = tableroRepository;
            _mapper = mapper;
        }

        public async Task<TableroDTO> Handle(GetTableroByIdQuery request, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(request.Id);

            if (tablero == null)
            {
                throw new Exception($"Tablero con ID {request.Id} no encontrado.");
            }
                

            var tableroDTO = _mapper.Map<TableroDTO>(tablero);

            return tableroDTO;
        }
    }
}

