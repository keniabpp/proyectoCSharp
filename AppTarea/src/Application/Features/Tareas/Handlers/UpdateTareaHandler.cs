using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Application.Interfaces;

namespace Application.Features.Tareas.Handlers
{
    public class UpdateTareaHandler : IRequestHandler<UpdateTareaCommand, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _usuarioService;

        public UpdateTareaHandler(ITareaRepository tareaRepository, IMapper mapper, IApplicationUserService usuarioService)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<TareaDTO> Handle(UpdateTareaCommand request, CancellationToken cancellationToken)
        {
            var tareaExistente = await _tareaRepository.GetByIdAsync(request.Id);
           

            // Validar que el usuario autenticado sea el creador
            if (tareaExistente!.creado_por != request.creado_por)
            {
                throw new InvalidOperationException($" Solo el creador puede actualizar la tarea");
            }
            _mapper.Map(request.TareaUpdateDTO, tareaExistente);
            await _tareaRepository.UpdateAsync(request.Id, tareaExistente);

            var tareaDTO = _mapper.Map<TareaDTO>(tareaExistente);
            
            // Poblar nombre_creador y nombre_asignado
            tareaDTO.nombre_creador = await _usuarioService.GetUserNameByIdAsync(tareaDTO.creado_por) ?? string.Empty;
            tareaDTO.nombre_asignado = await _usuarioService.GetUserNameByIdAsync(tareaDTO.asignado_a) ?? string.Empty;

            return tareaDTO;
        }
    }
}