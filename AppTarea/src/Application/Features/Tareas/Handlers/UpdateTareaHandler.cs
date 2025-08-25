using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Tareas.Handlers
{
    public class UpdateTareaHandler : IRequestHandler<UpdateTareaCommand, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        public UpdateTareaHandler(ITareaRepository tareaRepository, IMapper mapper)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
        }

        public async Task<TareaDTO> Handle(UpdateTareaCommand request, CancellationToken cancellationToken)
        {
            var tareaExistente = await _tareaRepository.GetByIdAsync(request.Id);
            if (tareaExistente == null)
            {
                throw new Exception($" No se encontró la tarea con ID {request.Id}");
            }
            _mapper.Map(request.TareaUpdateDTO, tareaExistente);
            await _tareaRepository.UpdateAsync(request.Id, tareaExistente);


            return _mapper.Map<TareaDTO>(tareaExistente);
        }
    }
}