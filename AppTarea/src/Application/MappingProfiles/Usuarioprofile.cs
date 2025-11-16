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

            // Creación: DTO → Entidad
            CreateMap<UsuarioCreateDTO, Usuario>();

            // Lectura: Entidad → DTO (excluir contraseña por seguridad)
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Contrasena, opt => opt.Ignore());

            // Mapeo específico para respuesta de registro (sin contraseña)
            CreateMap<Usuario, UsuarioRegisterResponseDTO>();

            // Actualización: DTO → Entidad 
            CreateMap<UsuarioUpdateDTO, Usuario>();
        }
    }
}
