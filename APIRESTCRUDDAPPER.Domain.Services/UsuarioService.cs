using APIRESTCRUDDAPPER.Domain.Entitys;
using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace APIRESTCRUDDAPPER.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> ObterUsuariosAsync()
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            // Dapper - Abre a conexão com o Banco de dados
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var retornoUsuariosDB = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

                if (retornoUsuariosDB.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário encontrado. Tente novamente!";
                    response.Status = false;

                    return response;
                }

                // AutoMapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(retornoUsuariosDB);

                response.Dados = usuarioMap;
                response.Mensagem = "Usuários retornados com sucesso";
            }

            return response;
        }

        public async Task<ResponseBase<UsuarioListarDto>> ObterUsuarioIdAsync(int usuarioId)
        {
            ResponseBase<UsuarioListarDto> response = new ResponseBase<UsuarioListarDto>();

            // Dapper - Abre a conexão com o Banco de dados
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var retornoUsuarioIdDB = await connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = usuarioId });

                if (retornoUsuarioIdDB is null)
                {
                    response.Mensagem = "Nenhum usuário encontrado com o Id informado. Tente novamente!";
                    response.Status = false;

                    return response;
                }

                // AutoMapper
                var usuarioIdMap = _mapper.Map<UsuarioListarDto>(retornoUsuarioIdDB);

                response.Dados = usuarioIdMap;
                response.Mensagem = "Usuário Id retornado com sucesso";
            }

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> AdicionarUsuarioAsync(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            // Dapper - Abre a conexão com o Banco de dados
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var retornoAdicionarUsuarioDB = await connection
                    .ExecuteAsync("INSERT INTO Usuarios(NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) VALUES (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", 
                    usuarioCriarDto);

                if (retornoAdicionarUsuarioDB == 0)
                {
                    response.Mensagem = "Não foi possível adicionar um novo usuário. Tente novamente!";
                    response.Status = false;

                    return response;
                }

                var retornoUsuariosAtualizadosDB = await ListUsuariosAsync(connection);

                // AutoMapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(retornoUsuariosAtualizadosDB);

                response.Dados = usuarioMap;
                response.Mensagem = "Usuário adicionado com sucesso";
            }

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> EditarUsuarioAsync(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            // Dapper - Abre a conexão com o Banco de dados
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var retornoAtualizarUsuarioDB = await connection
                    .ExecuteAsync("UPDATE Usuarios SET NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, Salario = @Salario, CPF = @CPF, Situacao = @Situacao " +
                    "WHERE Id = @Id", usuarioEditarDto);

                if (retornoAtualizarUsuarioDB == 0)
                {
                    response.Mensagem = "Não foi possível atualizar o usuário. Tente novamente!";
                    response.Status = false;

                    return response;
                }
                var retornoUsuariosAtualizadosDB = await ListUsuariosAsync(connection);

                // AutoMapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(retornoUsuariosAtualizadosDB);

                response.Dados = usuarioMap;
                response.Mensagem = "Usuário atualizado com sucesso.";

            }

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> DeletarUsuarioAsync(int usuarioId)
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            // Dapper - Abre a conexão com o Banco de dados
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var retornoDeletarUsuarioDB = await connection.ExecuteAsync("DELETE FROM Usuarios WHERE Id = @Id", new { Id = usuarioId });

                if (retornoDeletarUsuarioDB == 0)
                {
                    response.Mensagem = "Não foi possível deletar o usuário. Tente novamente!";
                    response.Status = false;

                    return response;
                }
                var retornoUsuariosAtualizadosDB = await ListUsuariosAsync(connection);

                // AutoMapper
                var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(retornoUsuariosAtualizadosDB);

                response.Dados = usuarioMap;
                response.Mensagem = "Usuário deletado com sucesso.";
            }

            return response;
        }

        #region Métodos Privados

        private static async Task<IEnumerable<Usuario>> ListUsuariosAsync(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
        }

        #endregion
    }
}
