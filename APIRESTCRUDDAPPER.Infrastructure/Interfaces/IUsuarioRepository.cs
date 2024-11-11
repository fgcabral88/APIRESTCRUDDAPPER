using APIRESTCRUDDAPPER.Domain.Entitys;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;

namespace APIRESTCRUDDAPPER.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface para repositório de usuário
    /// </summary>
    public interface IUsuarioRepository : IDisposable
    {
        Task<IEnumerable<UsuarioListarDto>> ObterUsuariosRepositorioAsync();
        Task<UsuarioListarDto> ObterUsuarioPorIdRepositorioAsync(int id);
        Task<ResponseBase<List<UsuarioListarDto>>> AdicionarUsuarioRepositorioAsync(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseBase<List<UsuarioListarDto>>> EditarUsuarioRespositorioAsync(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseBase<bool>> RemoverUsuarioRepositorioAsync(int id);
        Task<ResponseBase<List<Usuario>>> ListarUsuariosRepositorioAsync();
    }
}