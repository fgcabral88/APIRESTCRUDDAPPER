using APIRESTCRUDDAPPER.Domain.Entitys;
using APIRESTCRUDDAPPER.Dto;
using AutoMapper;

namespace APIRESTCRUDDAPPER.Application.Profiles.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            // Usuários
            CreateMap<Usuario, UsuarioCriarDto>().ReverseMap();
            CreateMap<Usuario, UsuarioEditarDto>().ReverseMap();
            CreateMap<Usuario, UsuarioListarDto>().ReverseMap();

            // Dtos
            CreateMap<UsuarioCriarDto, UsuarioListarDto>().ReverseMap();
            CreateMap<UsuarioEditarDto, UsuarioListarDto>().ReverseMap();
        }
    }
}
