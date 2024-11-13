using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Infrastructure.Interfaces;
using APIRESTCRUDDAPPER.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace APIRESTCRUDDAPPER.Domain.Services.Services
{
    public class UsuarioService : IUsuarioService 
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> ObterUsuariosAsync()
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            var usuariosDB = await _usuarioRepository.ObterUsuariosRepositorioAsync();

            if (!usuariosDB.Any())
            {
                response.Mensagem = "Nenhum usuário encontrado. Tente novamente!";
                response.Status = false;
                return response; 
            }

            var usuariosMap = _mapper.Map<List<UsuarioListarDto>>(usuariosDB);
            response.Dados = usuariosMap;
            response.Mensagem = "Usuários retornados com sucesso";
            response.Status = true;

            return response;
        }

        public async Task<ResponseBase<UsuarioListarDto>> ObterUsuarioIdAsync(int id)
        {
            ResponseBase<UsuarioListarDto> response = new ResponseBase<UsuarioListarDto>();

            var usuarioIdDB = await _usuarioRepository.ObterUsuarioPorIdRepositorioAsync(id);

            if (usuarioIdDB is null || usuarioIdDB.Id <= 0)
            {
                response.Mensagem = "Nenhum usuário encontrado com o Id informado. Tente novamente!";
                response.Status = false;
                return response; 
            }

            var usuarioIdMap = _mapper.Map<UsuarioListarDto>(usuarioIdDB);
            response.Dados = usuarioIdMap;
            response.Mensagem = "Usuário Id retornado com sucesso";
            response.Status = true;

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> AdicionarUsuarioAsync(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            var usuarioAdicionarDB = await _usuarioRepository.AdicionarUsuarioRepositorioAsync(usuarioCriarDto);

            if (!usuarioAdicionarDB.Status)
            {
                response.Mensagem = "Não foi possível adicionar um novo usuário. Refaça a operação novamente!";
                response.Status = false;
                return response;
            }

            var usuarioListarDB = await _usuarioRepository.ListarUsuariosRepositorioAsync();

            if (usuarioListarDB == null || !usuarioListarDB.Status)
            {
                response.Mensagem = "Usuário adicionado, mas houve um erro ao obter a lista atualizada de usuários.";
                response.Status = false;
                return response;
            }

            var usuariosMap = _mapper.Map<List<UsuarioListarDto>>(usuarioListarDB?.Dados?.OrderBy(x => x.Id).ToList());
            response.Dados = usuariosMap;
            response.Status = true;
            response.Mensagem = "Usuário adicionado com sucesso.";

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> EditarUsuarioAsync(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            var usuarioEditarDB = await _usuarioRepository.EditarUsuarioRespositorioAsync(usuarioEditarDto);

            if (!usuarioEditarDB.Status)
            {
                response.Mensagem = "Não foi possível editar o usuário. Refaça a operação novamente!";
                response.Status = false;
                return response; 
            }

            var usuariosAtualizado = await _usuarioRepository.ListarUsuariosRepositorioAsync();

            if (usuariosAtualizado == null || !usuariosAtualizado.Status)
            {
                response.Mensagem = "Usuário editado, mas houve um erro ao obter a lista atualizada de usuários.";
                response.Status = false;
                return response;
            }

            // Mapeia os usuários para o DTO e retorna o resultado
            response.Dados = usuariosAtualizado?.Dados?.OrderBy(x => x.Id).Select(usuario => new UsuarioListarDto
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                Cargo = usuario.Cargo,
                Salario = usuario.Salario,
                Situacao = usuario.Situacao
            }).ToList();

            response.Mensagem = "Usuário editado com sucesso";
            response.Status = true;

            return response;
        }

        public async Task<ResponseBase<List<UsuarioListarDto>>> DeletarUsuarioAsync(int id)
        {
            ResponseBase<List<UsuarioListarDto>> response = new ResponseBase<List<UsuarioListarDto>>();

            if (id <= 0)
            {
                response.Mensagem = "Id inválido! Não foi possível deletar o usuário. Tente novamente!";
                response.Status = false;
                return response;
            }

            var usuarioDeletarDB = await _usuarioRepository.RemoverUsuarioRepositorioAsync(id);

            if (!usuarioDeletarDB.Status)
            {
                response.Mensagem = "Não foi possível deletar o usuário. Tente novamente!";
                response.Status = false;
                return response;
            }

            // Se o usuário foi deletado, obtém a lista atualizada de usuários
            var usuariosAtualizados = await _usuarioRepository.ListarUsuariosRepositorioAsync();

            if (usuariosAtualizados == null || !usuariosAtualizados.Status)
            {
                response.Mensagem = "Usuário deletado, mas houve um erro ao obter a lista atualizada de usuários.";
                response.Status = false;
                return response;
            }

            // Mapeia os usuários para o DTO e retorna o resultado
            response.Dados = usuariosAtualizados?.Dados?.OrderBy(x => x.Id).Select(usuario => new UsuarioListarDto
                {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                Cargo = usuario.Cargo,
                Salario = usuario.Salario,
                Situacao = usuario.Situacao
                }).ToList();

            response.Mensagem = "Usuário deletado com sucesso";
            response.Status = true;

            return response;
        }
    }
}
