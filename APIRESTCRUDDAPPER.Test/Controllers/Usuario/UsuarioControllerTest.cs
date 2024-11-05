using Moq;
using Microsoft.AspNetCore.Mvc;
using APIRESTCRUDDAPPER.Controllers;
using APIRESTCRUDDAPPER.Dto;
using APIRESTCRUDDAPPER.Models;
using APIRESTCRUDDAPPER.Services;

namespace APIRESTCRUDDAPPER.Test.Controllers.UsuarioControllerTest
{
    public class UsuarioControllerTest
    {
        private readonly UsuarioController _usuarioController;
        private readonly Mock<IUsuarioInterface> _mockService;

        public UsuarioControllerTest()
        {
            _mockService = new Mock<IUsuarioInterface>();
            _usuarioController = new UsuarioController(_mockService.Object);
        }

        #region Obter Usuários Testes

        [Fact]
        public async Task ObterUsuariosAsync_ReturnsOkResult_WhenStatusIsTrue()
        {
            // Arrange
            var usuariosDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Dados = new List<UsuarioListarDto> { new UsuarioListarDto { Id = 1, NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, Situacao = true } },
                Status = true
            };

            _mockService.Setup(service => service.ObterUsuariosAsync()).ReturnsAsync(usuariosDto);

            // Act
            var result = await _usuarioController.ObterUsuariosAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(okResult.Value);

            Assert.True(returnValue.Status);
            Assert.NotNull(returnValue.Dados);
            Assert.Single(returnValue.Dados);
        }

        [Fact]
        public async Task ObterUsuariosAsync_ReturnsNotFound_WhenStatusIsFalse()
        {
            // Arrange
            var usuariosDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Status = false,
                Dados = null
            };
            _mockService.Setup(service => service.ObterUsuariosAsync())
                        .ReturnsAsync(usuariosDto);

            // Act
            var result = await _usuarioController.ObterUsuariosAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(notFoundResult.Value);

            Assert.False(returnValue.Status);
            Assert.Null(returnValue.Dados);
        }

        #endregion

        #region Obter Usuário por Id Testes

        [Fact]
        public async Task ObterUsuarioIdAsync_ReturnsOkResult_WhenStatusIsTrue()
        {
            // Arrange
            int usuarioId = 1;
            var usuarioDto = new ResponseModel<UsuarioListarDto>
            {
                Dados = new UsuarioListarDto { Id = 1, NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, Situacao = true },
                Status = true
            };

            _mockService.Setup(service => service.ObterUsuarioIdAsync(usuarioId)).ReturnsAsync(usuarioDto);

            // Act
            var result = await _usuarioController.ObterUsuarioIdAsync(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<UsuarioListarDto>>(okResult.Value);

            Assert.True(returnValue.Status);
            Assert.NotNull(returnValue.Dados);
            Assert.Equal("Usuário Teste", returnValue.Dados.NomeCompleto);
        }

        [Fact]
        public async Task ObterUsuarioIdAsync_ReturnsNotFound_WhenStatusIsFalse()
        {
            // Arrange
            int usuarioId = 1;
            var usuarioDto = new ResponseModel<UsuarioListarDto>
            {
                Status = false,
                Dados = null
            };

            _mockService.Setup(service => service.ObterUsuarioIdAsync(usuarioId)).ReturnsAsync(usuarioDto);

            // Act
            var result = await _usuarioController.ObterUsuarioIdAsync(usuarioId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<UsuarioListarDto>>(notFoundResult.Value);

            Assert.False(returnValue.Status);
            Assert.Null(returnValue.Dados);
        }

        #endregion

        #region Adicionar Usuário Testes

        [Fact]
        public async Task AdicionarUsuarioAsync_ReturnsOkResult_WhenStatusIsTrue()
        {
            // Arrange
            var usuarioCriarDto = new UsuarioCriarDto { NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, CPF = "11111111111", Senha = "12345",  Situacao = true };
            var usuarioDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Dados = new List<UsuarioListarDto> { new UsuarioListarDto { NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, Situacao = true } },
                Status = true
            };

            _mockService.Setup(service => service.AdicionarUsuarioAsync(usuarioCriarDto)).Returns(Task.FromResult(usuarioDto));

            // Act
            var result = await _usuarioController.AdicionarUsuarioAsync(usuarioCriarDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(okResult.Value);

            Assert.True(returnValue.Status);
            Assert.NotNull(returnValue.Dados);
            Assert.Single(returnValue.Dados);
            Assert.Equal("Usuário Teste", returnValue.Dados[0].NomeCompleto);
        }

        [Fact]
        public async Task AdicionarUsuarioAsync_ReturnsBadRequest_WhenStatusIsFalse()
        {
            // Arrange
            var usuarioCriarDto = new UsuarioCriarDto { NomeCompleto = "Usuário Inválido" };
            var usuarioDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Dados = null,
                Status = false
            };

            _mockService.Setup(service => service.AdicionarUsuarioAsync(usuarioCriarDto)).Returns(Task.FromResult(usuarioDto));

            // Act
            var result = await _usuarioController.AdicionarUsuarioAsync(usuarioCriarDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(badRequestResult.Value);

            Assert.False(returnValue.Status);
            Assert.Null(returnValue.Dados);
        }

        #endregion

        #region Editar Usuário Testes

        [Fact]
        public async Task EditarUsuarioAsync_ReturnsOkResult_WhenStatusIsTrue()
        {
            // Arrange
            var usuarioEditarDto = new UsuarioEditarDto { NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, CPF = "11111111111", Situacao = true };
            var usuarioDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Dados = new List<UsuarioListarDto> { new UsuarioListarDto { NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, Situacao = true } },
                Status = true
            };

            _mockService.Setup(service => service.EditarUsuarioAsync(usuarioEditarDto)).ReturnsAsync(usuarioDto);

            // Act
            var result = await _usuarioController.EditarUsuarioAsync(usuarioEditarDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(okResult.Value);

            Assert.True(returnValue.Status);
            Assert.NotNull(returnValue.Dados);
            Assert.Single(returnValue.Dados);
            Assert.Equal("Usuário Teste", returnValue.Dados[0].NomeCompleto);
        }

        [Fact]
        public async Task EditarUsuarioAsync_ReturnsBadRequest_WhenStatusIsFalse()
        {
            // Arrange
            var usuarioEditarDto = new UsuarioEditarDto { NomeCompleto = "Usuário Inválido" };
            var usuarioDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Status = false,
                Dados = null
            };

            _mockService.Setup(service => service.EditarUsuarioAsync(usuarioEditarDto)).ReturnsAsync(usuarioDto);

            // Act
            var result = await _usuarioController.EditarUsuarioAsync(usuarioEditarDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(badRequestResult.Value);

            Assert.False(returnValue.Status);
            Assert.Null(returnValue.Dados);
        }

        #endregion

        #region Deletar Usuário Testes

        [Fact]
        public async Task DeletarUsuarioAsync_ReturnsOkResult_WhenStatusIsTrue()
        {
            // Arrange
            int usuarioId = 1;
            var usuarioDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Status = true,
                Dados = new List<UsuarioListarDto> { new UsuarioListarDto { NomeCompleto = "Usuário Teste", Cargo = "Teste", Email = "test@teste.com", Salario = 1000, Situacao = true } } //NomeCompleto = "Usuário Removido" } }
            };

            _mockService.Setup(service => service.DeletarUsuarioAsync(usuarioId)).ReturnsAsync(usuarioDto);

            // Act
            var result = await _usuarioController.DeletarUsuarioAsync(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(okResult.Value);

            Assert.True(returnValue.Status);
            Assert.NotNull(returnValue.Dados);
            Assert.Single(returnValue.Dados);
            Assert.Equal("Usuário Teste", returnValue.Dados[0].NomeCompleto);
        }

        [Fact]
        public async Task DeletarUsuarioAsync_ReturnsNotFound_WhenStatusIsFalse()
        {
            // Arrange
            int usuarioId = 2;
            var usuarioDto = new ResponseModel<List<UsuarioListarDto>>
            {
                Status = false,
                Dados = null
            };

            _mockService.Setup(service => service.DeletarUsuarioAsync(usuarioId)).ReturnsAsync(usuarioDto);

            // Act
            var result = await _usuarioController.DeletarUsuarioAsync(usuarioId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<ResponseModel<List<UsuarioListarDto>>>(notFoundResult.Value);

            Assert.False(returnValue.Status);
            Assert.Null(returnValue.Dados);
        }

        #endregion
    }
}


