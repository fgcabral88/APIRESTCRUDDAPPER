using APIRESTCRUDDAPPER.Domain.Entitys;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;
using AutoMapper;

namespace APIRESTCRUDDAPPER.Application.Profiles.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            // Usuários
            CreateMap<Usuario, UsuarioListarDto>().ReverseMap();
            CreateMap<Usuario, UsuarioCriarDto>().ReverseMap();
            CreateMap<Usuario, UsuarioEditarDto>().ReverseMap();

            //Dtos
            CreateMap<UsuarioCriarDto, UsuarioListarDto>().ReverseMap();
            CreateMap<UsuarioEditarDto, UsuarioListarDto>().ReverseMap();

            CreateMap<ResponseBase<List<Usuario>>, ResponseBase<List<UsuarioListarDto>>>()
                .ForMember(dest => dest.Dados, opt => opt.MapFrom(src => src.Dados));
        }
    }
}
