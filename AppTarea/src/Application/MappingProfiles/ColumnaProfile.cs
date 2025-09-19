using Application.Features.Columnas.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class ColumnaProfile : Profile
    {
        public ColumnaProfile()
        {
            CreateMap<Columna, ColumnaDTO>()
            .ForMember(dest => dest.id_columna, opt => opt.MapFrom(src => src.id_columna));
        }
    }
}
