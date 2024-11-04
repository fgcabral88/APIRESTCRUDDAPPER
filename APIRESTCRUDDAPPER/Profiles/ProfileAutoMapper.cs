using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;
using AutoMapper;

namespace APIRESTCRUDDAPPER.Profiles
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
