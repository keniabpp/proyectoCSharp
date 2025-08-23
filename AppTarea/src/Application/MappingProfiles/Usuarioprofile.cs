// Application/Features/Usuarios/Mapping/UsuarioProfile.cs
using AutoMapper;
using Application.Features.Usuarios.DTOs;
using Domain.Entities;

namespace Application.Features.Usuarios.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioRegisterDTO, Usuario>();

            // 🟡 Creación: DTO → Entidad
            CreateMap<UsuarioCreateDTO, Usuario>();

            // 🟢 Lectura: Entidad → DTO (si lo necesitas)
            CreateMap<Usuario, UsuarioDTO>();

            // 🔵 Actualización: DTO → Entidad (si lo necesitas)
            CreateMap<UsuarioUpdateDTO, Usuario>();
        }
    }
}
