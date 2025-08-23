using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Tareas.Handlers
{
    public class GetTareaByIdHandler : IRequestHandler<GetTareaByIdQuery, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        public GetTareaByIdHandler(ITareaRepository tareaRepository, IMapper mapper)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
        }

        public async Task<TareaDTO> Handle(GetTareaByIdQuery request, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(request.Id);

            if (tarea == null)
                throw new Exception("Tarea no encontrada");

            var tareaDTO = _mapper.Map<TareaDTO>(tarea);


            return tareaDTO;
        }
    }
}

