using APIRESTCRUDDAPPER.Domain.Interfaces;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;

namespace APIRESTCRUDDAPPER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioInterfaceService;

        public UsuarioController(IUsuarioService usuarioInterfaceService)
        {
            _usuarioInterfaceService = usuarioInterfaceService;
        }

        /// <summary>
        /// Obter todos os Usuários.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna uma lista de todos os usuários cadastrados.
        /// </remarks>
        /// <returns> Lista de Usuários </returns>
        /// <response code="200"> Lista de usuários retornada com sucesso.</response>
        /// <response code="404"> Lista de usuários não retornada.</response>
        /// <response code="400"> Lista de usuários não retornada.</response>
        [HttpGet]
        [Route("ObterUsuariosAsync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterUsuariosAsync()
        {
            var cronometro = Stopwatch.StartNew();
            var usuarios = await _usuarioInterfaceService.ObterUsuariosAsync();

            if (!usuarios.Status)
            {
                Log.Error("LOG {Mensagem} - Tempo: {Tempo} ms", usuarios.Mensagem, cronometro.ElapsedMilliseconds);
                return NotFound(usuarios);
            }

            Log.Information("LOG {Mensagem} - Tempo: {Tempo} ms", usuarios.Mensagem, cronometro.ElapsedMilliseconds);
            return Ok(usuarios);
        }

        /// <summary>
        /// Obter Usuário por Id.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna o usuário pelo seu Id cadastrado.
        /// </remarks>
        /// <param name="Id"></param>
        /// <returns> Usuário Id </returns>
        /// <response code="200"> Retorno do usuário pelo Id com sucesso.</response>
        /// <response code="404"> Usuário não encontrado.</response>
        /// <response code="400"> Usuário não encontrado.</response>
        [HttpGet]
        [Route("{Id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterUsuarioIdAsync(int Id)
        {
            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.ObterUsuarioIdAsync(Id);

            if (!usuario.Status)
            {
                Log.Error("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
                return NotFound(usuario);
            }

            Log.Information("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
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
        /// <response code="404"> Usuário não adicionado.</response>
        /// <response code="400"> Usuário não adicionado.</response>    
        [HttpPost]
        [Route("AdicionarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarUsuarioAsync([FromBody] UsuarioCriarDto usuarioCriarDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.AdicionarUsuarioAsync(usuarioCriarDto);

            if (!usuario.Status)
            {
                Log.Error("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
                return BadRequest(usuario);
            }

            Log.Information("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
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
        /// <response code="404"> Usuário não editado.</response>
        /// <response code="400"> Usuário não editado.</response>
        [HttpPut]
        [Route("EditarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditarUsuarioAsync([FromBody] UsuarioEditarDto usuarioEditarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.EditarUsuarioAsync(usuarioEditarDto);

            if (!usuario.Status)
            {
                Log.Error("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
                return BadRequest(usuario);
            }

            Log.Information("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
            return Ok(usuario);
        }

        /// <summary>
        /// Deletar Usuário.
        /// </summary>
        /// <remarks>
        /// Este endpoint deleta um usuário.
        /// </remarks>
        /// <param name="Id"></param>
        /// <returns> Usuário deletado </returns>
        /// <response code="200"> Usuário deletado com sucesso.</response>
        /// <response code="404"> Usuário não deletado.</response>
        /// <response code="400"> Usuário não deletado.</response>
        [HttpDelete]
        [Route("DeletarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletarUsuarioAsync(int Id)
        {
            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.DeletarUsuarioAsync(Id);

            if (!usuario.Status)
            {
                Log.Error("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
                return NotFound(usuario);
            }

            Log.Information("LOG {Mensagem} - Tempo: {Tempo} ms", usuario.Mensagem, cronometro.ElapsedMilliseconds);
            return Ok(usuario);
        }
    }
}
