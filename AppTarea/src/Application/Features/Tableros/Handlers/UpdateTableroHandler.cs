using Application.Features.Tableros.Commands;
using Application.Features.Tableros.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Tableros.Handlers
{
    public class UpdateTableroHandler : IRequestHandler<UpdateTableroCommand, TableroDTO>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IMapper _mapper;

        public UpdateTableroHandler(ITableroRepository tableroRepository, IMapper mapper)
        {
            _tableroRepository = tableroRepository;
            _mapper = mapper;
        }


        public async Task<TableroDTO> Handle(UpdateTableroCommand request, CancellationToken cancellationToken)
        {
            var tableroExistente = await _tableroRepository.GetByIdAsync(request.Id);
            _mapper.Map(request.TableroUpdateDTO, tableroExistente);

            await _tableroRepository.UpdateAsync(request.Id, tableroExistente!);
            
            return _mapper.Map<TableroDTO>(tableroExistente);
        }
    }

    
}