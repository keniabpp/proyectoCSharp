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
        private readonly IColumnaRepository _columnaRepository;
        private readonly IMapper _mapper;

        public CreateTableroHandler(ITableroRepository tableroRepository,IColumnaRepository columnaRepository, IMapper mapper)
        {
            _tableroRepository = tableroRepository;
            _columnaRepository = columnaRepository;
            _mapper = mapper;
        }
        public async Task<TableroDTO> Handle(CreateTableroCommand request, CancellationToken cancellationToken)
        {
            var tablero = _mapper.Map<Tablero>(request.TableroCreateDTO);
            var tableroCreado = await _tableroRepository.CreateAsync(tablero);

            // Crear las columnas por defecto
            var columnasPorDefecto = new List<Columna>
            {
                new Columna("Por hacer", EstadoColumna.PorHacer, tableroCreado.id_tablero),
                new Columna("En progreso", EstadoColumna.EnProgreso, tableroCreado.id_tablero),
                new Columna("Hecho", EstadoColumna.Hecho, tableroCreado.id_tablero)
            };

            foreach (var columna in columnasPorDefecto)
            {
                await _columnaRepository.CreateAsync(columna);
            }

            var tableroDTO = _mapper.Map<TableroDTO>(tableroCreado);
            return tableroDTO;
        }  
    }
}