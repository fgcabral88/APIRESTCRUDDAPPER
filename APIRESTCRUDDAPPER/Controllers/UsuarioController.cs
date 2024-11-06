using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTCRUDDAPPER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterfaceService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioInterface usuarioInterfaceService, ILogger<UsuarioController> logger)
        {
            _usuarioInterfaceService = usuarioInterfaceService;
            _logger = logger;
        }

        /// <summary>
        /// Obter todos os Usuários.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna uma lista de todos os usuários cadastrados.
        /// </remarks>
        /// <returns> Lista de Usuários </returns>
        /// <response code="200"> Lista de usuários retornada com sucesso.</response>
        [HttpGet]
        [Route("ObterUsuariosAsync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), 200)]
        public async Task<IActionResult> ObterUsuariosAsync()
        {
            var usuarios = await _usuarioInterfaceService.ObterUsuariosAsync();

            if (usuarios.Status == false)
            {
                _logger.LogError(usuarios.Mensagem);
                return NotFound(usuarios);
            }

            _logger.LogInformation(usuarios.Mensagem);
            return Ok(usuarios);
        }

        /// <summary>
        /// Obter Usuário por Id.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna o usuário pelo seu Id cadastrado.
        /// </remarks>
        /// <param name="usuarioId"></param>
        /// <returns> Usuário Id </returns>
        /// <response code="200"> Retorno do usuário pelo Id com sucesso.</response>
        [HttpGet]
        [Route("{usuarioId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), 200)]
        public async Task<IActionResult> ObterUsuarioIdAsync(int usuarioId)
        {
            var usuario = await _usuarioInterfaceService.ObterUsuarioIdAsync(usuarioId);

            if (usuario.Status == false)
            {
                _logger.LogError(usuario.Mensagem);
                return NotFound(usuario);
            }

            _logger.LogInformation(usuario.Mensagem);
            return Ok(usuario);
        }

        /// <summary>
        /// Adicionar Usuário.
        /// </summary>
        /// <remarks>
        /// Este endpoint adiciona um usuário.
        /// </remarks>
        /// <param name="usuarioCriarDto"></param>
        /// <returns> Adicionar usuário </returns>
        /// <response code="200"> Usuário adicionado com sucesso.</response>
        [HttpPost]
        [Route("AdicionarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), 200)]
        public async Task<IActionResult> AdicionarUsuarioAsync(UsuarioCriarDto usuarioCriarDto)
        {
            var usuario = await _usuarioInterfaceService.AdicionarUsuarioAsync(usuarioCriarDto);

            if (usuario.Status == false)
            {
                _logger.LogError(usuario.Mensagem);
                return BadRequest(usuario);
            }

            _logger.LogInformation(usuario.Mensagem);
            return Ok(usuario);
        }

        /// <summary>
        /// Editar Usuário.
        /// </summary>
        /// <remarks>
        /// Este endpoint edita um usuário.
        /// </remarks>
        /// <param name="usuarioEditarDto"></param>
        /// <returns> Usuário editado </returns>
        /// <response code="200"> Usuário editado com sucesso.</response>
        [HttpPut]
        [Route("EditarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), 200)]
        public async Task<IActionResult> EditarUsuarioAsync(UsuarioEditarDto usuarioEditarDto)
        {
            var usuario = await _usuarioInterfaceService.EditarUsuarioAsync(usuarioEditarDto);

            if (usuario.Status == false)
            {
                _logger.LogError(usuario.Mensagem);
                return BadRequest(usuario);
            }

            _logger.LogInformation(usuario.Mensagem);
            return Ok(usuario);
        }

        /// <summary>
        /// Deletar Usuário.
        /// </summary>
        /// <remarks>
        /// Este endpoint deleta um usuário.
        /// </remarks>
        /// <param name="usuarioId"></param>
        /// <returns> Usuário deletado </returns>
        /// <response code="200"> Usuário editado com sucesso.</response>
        [HttpDelete]
        [Route("DeletarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), 200)]
        public async Task<IActionResult> DeletarUsuarioAsync(int usuarioId)
        {
            var usuario = await _usuarioInterfaceService.DeletarUsuarioAsync(usuarioId);

            if (usuario.Status == false)
            {
                _logger.LogError(usuario.Mensagem);
                return NotFound(usuario);
            }

            _logger.LogInformation(usuario.Mensagem);
            return Ok(usuario);
        }
    }
}
