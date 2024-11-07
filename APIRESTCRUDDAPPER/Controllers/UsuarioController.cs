﻿using APIRESTCRUDDAPPER.Domain.Interfaces;
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
        private readonly IUsuarioInterface _usuarioInterfaceService;

        public UsuarioController(IUsuarioInterface usuarioInterfaceService)
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
        /// <response code="404"> Lista de usuários não encontrada.</response>
        /// <response code="400"> Lista de usuários não encontrada.</response>
        /// <response code="500"> Erro interno.</response>
        [HttpGet]
        [Route("ObterUsuariosAsync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBase<List<UsuarioListarDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterUsuariosAsync()
        {
            var cronometro = Stopwatch.StartNew();
            var usuarios = await _usuarioInterfaceService.ObterUsuariosAsync();

            if (usuarios.Status is false)
            {
                Log.Error($"LOG Mensagem: {usuarios.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
                return NotFound(usuarios);
            }

            Log.Information($"LOG Mensagem: {usuarios.Mensagem} | Tempo: {cronometro.Elapsed} ms");
            return Ok(usuarios);
        }

        /// <summary>
        /// Obter Usuário por Id.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna o usuário pelo seu Id cadastrado.
        /// </remarks>
        /// <param name="usuarioId">Id do usuário</param>
        /// <returns> Usuário Id </returns>
        /// <response code="200"> Retorno do usuário pelo Id com sucesso.</response>
        /// <response code="404"> Usuário não encontrado.</response>
        /// <response code="400"> Usuário não encontrado.</response>
        /// <response code="500"> Erro interno.</response>
        [HttpGet]
        [Route("{usuarioId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterUsuarioIdAsync(int usuarioId)
        {
            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.ObterUsuarioIdAsync(usuarioId);

            if (usuario.Status is false)
            {
                Log.Error($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
                return NotFound(usuario);
            }

            Log.Information($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
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
        /// <response code="500"> Erro interno.</response>
        [HttpPost]
        [Route("AdicionarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdicionarUsuarioAsync([FromBody] UsuarioCriarDto usuarioCriarDto)
        {
            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.AdicionarUsuarioAsync(usuarioCriarDto);

            if (usuario.Status is false)
            {
                Log.Error($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
                return BadRequest(usuario);
            }

            Log.Information($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
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
        /// <response code="500"> Erro interno.</response>
        [HttpPut]
        [Route("EditarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarUsuarioAsync([FromBody] UsuarioEditarDto usuarioEditarDto)
        {
            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.EditarUsuarioAsync(usuarioEditarDto);

            if (usuario.Status is false)
            {
                Log.Error($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
                return BadRequest(usuario);
            }

            Log.Information($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
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
        /// <response code="200"> Usuário deletado com sucesso.</response>
        /// <response code="404"> Usuário não deletado.</response>
        /// <response code="400"> Usuário não deletado.</response>
        /// <response code="500"> Erro interno.</response>
        [HttpDelete]
        [Route("DeletarUsuarioAsync")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarUsuarioAsync(int usuarioId)
        {
            var cronometro = Stopwatch.StartNew();
            var usuario = await _usuarioInterfaceService.DeletarUsuarioAsync(usuarioId);

            if (usuario.Status is false)
            {
                Log.Error($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
                return NotFound(usuario);
            }

            Log.Information($"LOG Mensagem: {usuario.Mensagem} | Tempo: {cronometro.ElapsedMilliseconds} ms");
            return Ok(usuario);
        }
    }
}
