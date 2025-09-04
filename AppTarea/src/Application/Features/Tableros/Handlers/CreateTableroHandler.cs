using Application.Features.Tableros.Commands;
using Application.Features.Tableros.DTOs;
using Application.Features.Columnas.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tableros.Handlers
{
    public class CreateTableroHandler : IRequestHandler<CreateTableroCommand, TableroDTO>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IMapper _mapper;

        public CreateTableroHandler(ITableroRepository tableroRepository, IMapper mapper)
        {
            _tableroRepository = tableroRepository;
            _mapper = mapper;
        }
        public async Task<TableroDTO> Handle(CreateTableroCommand request, CancellationToken cancellationToken)
        {
            var tablero = _mapper.Map<Tablero>(request.TableroCreateDTO);
            var tableroCreado = await _tableroRepository.CreateAsync(tablero);

            var tableroDTO = _mapper.Map<TableroDTO>(tableroCreado);
            return tableroDTO;
        }  
    }
}