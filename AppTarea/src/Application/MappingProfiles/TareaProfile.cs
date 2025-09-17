using AutoMapper;
using Application.Features.Tareas.DTOs;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class TareaProfile : Profile
    {
        public TareaProfile()
        {
        //     // 🟢 Lectura: Entidad → DTO
           CreateMap<Tarea, TareaDTO>()
              .ForMember(dest => dest.creado_por, opt => opt.MapFrom(src => src.creado_por))
              .ForMember(dest => dest.asignado_a, opt => opt.MapFrom(src => src.asignado_a))
              .ForMember(dest => dest.nombre_creador, opt => opt.MapFrom(src => src.creador.nombre))
              .ForMember(dest => dest.nombre_asignado, opt => opt.MapFrom(src => src.asignado.nombre))
              .ForMember(dest => dest.nombre_columna, opt => opt.MapFrom(src => src.columna.nombre))
              .ForMember(dest => dest.nombre_tablero, opt => opt.MapFrom(src => src.tablero.nombre))
              .ForMember(dest => dest.estado_fechaVencimiento, opt =>opt.MapFrom(src =>
              src.columna.nombre.ToLower() == "hecho"? "Completada":
              (src.fecha_vencimiento < DateTime.Now ? "Tarea vencida" : "Dentro del plazo")));




            // 🟡 Creación: DTO → Entidad
            CreateMap<TareaCreateDTO, Tarea>()
               .ForMember(dest => dest.creado_por, opt => opt.MapFrom(src => src.creado_por))
               .ForMember(dest => dest.asignado_a, opt => opt.MapFrom(src => src.asignado_a));
               
            // 🔵 Actualización: DTO → Entidad existente
            CreateMap<TareaUpdateDTO, Tarea>();
        }
    }
}
