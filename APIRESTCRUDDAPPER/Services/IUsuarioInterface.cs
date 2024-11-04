using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;

namespace APIRESTCRUDDAPPER.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDto>>> ObterUsuariosAsync();
        Task<ResponseModel<UsuarioListarDto>> ObterUsuarioIdAsync(int usuarioId);
        Task<ResponseModel<List<UsuarioListarDto>>> AdicionarUsuarioAsync(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuarioAsync(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> DeletarUsuarioAsync(int usuarioId);
    }
}
