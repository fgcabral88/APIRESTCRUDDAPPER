using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;

namespace APIRESTCRUDDAPPER.Domain.Interfaces
{
    public interface IUsuarioInterface
    {
        Task<ResponseBase<List<UsuarioListarDto>>> ObterUsuariosAsync();
        Task<ResponseBase<UsuarioListarDto>> ObterUsuarioIdAsync(int Id);
        Task<ResponseBase<List<UsuarioListarDto>>> AdicionarUsuarioAsync(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseBase<List<UsuarioListarDto>>> EditarUsuarioAsync(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseBase<List<UsuarioListarDto>>> DeletarUsuarioAsync(int Id);
    }
}
