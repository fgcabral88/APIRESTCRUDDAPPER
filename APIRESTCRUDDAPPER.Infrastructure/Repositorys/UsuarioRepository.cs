using APIRESTCRUDDAPPER.Domain.Entitys;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Infrastructure.Interfaces;
using APIRESTCRUDDAPPER.Models;
using Dapper;
using System.Data;

namespace APIRESTCRUDDAPPER.Infrastructure.Repositorys
{
    public class UsuarioRepository : IUsuarioRepository, IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private bool _disposed = false;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UsuarioListarDto>> ObterUsuariosRepositorioAsync()
        {
            var query = "SELECT * FROM Usuarios";

            return await _dbConnection.QueryAsync<UsuarioListarDto>(query);
        }

        public async Task<UsuarioListarDto?> ObterUsuarioPorIdRepositorioAsync(int id)
        {
            var query = "SELECT * FROM Usuarios WHERE Id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<UsuarioListarDto>(query, new { Id = id });
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> AdicionarUsuarioRepositorioAsync(UsuarioCriarDto usuarioCriarDto)
        {
            var response = new ResponseBase<List<UsuarioListarDto>>();
            var query = "INSERT INTO Usuarios(NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) VALUES (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)";

            var linhasAfetadas = await _dbConnection.ExecuteAsync(query, new
            {
                NomeCompleto = usuarioCriarDto.NomeCompleto,
                Email = usuarioCriarDto.Email,
                Cargo = usuarioCriarDto.Cargo,
                Salario = usuarioCriarDto.Salario,
                CPF = usuarioCriarDto.CPF,
                Situacao = usuarioCriarDto.Situacao,
                Senha = usuarioCriarDto.Senha
            });

            if (linhasAfetadas > 0)
            {
                response.Status = true;
                response.Mensagem = "Usuário adicionado com sucesso.";
            }
            else
            {
                response.Status = false;
                response.Mensagem = "Não foi possível adicionar o usuário.";
            }

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> EditarUsuarioRespositorioAsync(UsuarioEditarDto usuarioEditarDto)
        {
            var response = new ResponseBase<List<UsuarioListarDto>>();
            var query = "UPDATE Usuarios SET NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, Salario = @Salario, CPF = @CPF, Situacao = @Situacao WHERE Id = @Id";

            var linhasAfetadas = await _dbConnection.ExecuteAsync(query, new
            {
                NomeCompleto = usuarioEditarDto.NomeCompleto,
                Email = usuarioEditarDto.Email,
                Cargo = usuarioEditarDto.Cargo,
                Salario = usuarioEditarDto.Salario,
                CPF = usuarioEditarDto.CPF,
                Situacao = usuarioEditarDto.Situacao,
                Id = usuarioEditarDto.Id
            });

            if (linhasAfetadas > 0)
            {
                response.Status = true;
                response.Mensagem = "Usuário editado com sucesso.";
            }
            else
            {
                response.Status = false;
                response.Mensagem = "Nenhum usuário foi encontrado com o Id informado.";
            }

            return response;
        }

        public async Task<ResponseBase<bool>> RemoverUsuarioRepositorioAsync(int id)
        {
            var response = new ResponseBase<bool>();
            var query = "DELETE FROM Usuarios WHERE Id = @Id";

            var linhasAfetadas = await _dbConnection.ExecuteAsync(query, new { Id = id });

            if (linhasAfetadas > 0)
            {
                response.Status = true;
                response.Mensagem = "Usuário deletado com sucesso.";
                response.Dados = true; 
            }
            else
            {
                response.Status = false;
                response.Mensagem = "Usuário não encontrado ou não foi possível deletar.";
                response.Dados = false; 
            }

            return response;
        }

        public async Task<ResponseBase<List<Usuario>>> ListarUsuariosRepositorioAsync()
        {
            const string query = "SELECT * FROM Usuarios";

            if (_dbConnection == null)
                return new ResponseBase<List<Usuario>> 
                { 
                    Mensagem = "Erro de conexão com o banco de dados",
                    Status = false
                };

            try
            {
                var usuarios = (await _dbConnection.QueryAsync<Usuario>(query)).ToList();

                return new ResponseBase<List<Usuario>>
                {
                    Dados = usuarios,
                    Mensagem = "Usuários listados com sucesso",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Usuario>>
                {
                    Mensagem = $"Erro ao listar usuários: {ex.Message}",
                    Status = false
                };
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _dbConnection?.Dispose();
                _disposed = true;
            }
        }
    }
}
