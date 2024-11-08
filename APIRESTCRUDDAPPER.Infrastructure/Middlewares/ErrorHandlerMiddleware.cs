using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;

namespace APIRESTCRUDDAPPER.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocorreu um erro inesperado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        #region Métodos privados auxiliares

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode = exception switch // Define o status HTTP dependendo do tipo de exceção
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = exception?.Message ?? "Ocorreu um erro inesperado. Tente novamente mais tarde." // Mensagem de resposta padronizada
            });

            return context.Response.WriteAsync(result);
        }

        #endregion
    }
}
