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
            CreateMap<Usuario, UsuarioListarDto>().ReverseMap();
        }
    }
}
