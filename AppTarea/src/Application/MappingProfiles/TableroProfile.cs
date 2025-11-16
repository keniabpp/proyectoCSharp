
using AutoMapper;
using Application.Features.Tableros.DTOs;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class TableroProfile : Profile
    {
        public TableroProfile()
        {
            // Lectura: Entidad → DTO
            CreateMap<Tablero, TableroDTO>()
            .ForMember(dest => dest.nombre_usuario, opt => opt.Ignore()) // Se configurará en el handler con lógica de AspNetUsers
            .ForMember(dest => dest.nombre_rol, opt => opt.Ignore()); // Se configurará en el handler con lógica de AspNetRoles

            // Creación: DTO → Entidad
            CreateMap<TableroCreateDTO, Tablero>()
            .ForMember(dest => dest.id_rol, opt => opt.MapFrom(src => src.id_rol))
            .ForMember(dest => dest.creado_por, opt => opt.MapFrom(src => src.creado_por));

            // Actualización: DTO → Entidad existente
            CreateMap<TableroUpdateDTO, Tablero>();
        }
    }
}
