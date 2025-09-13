using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Presentation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    "Error de validación",
                    ex.Errors.Select(e => new { Campo = e.PropertyName, Mensaje = e.ErrorMessage })
                );
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (ApplicationException ex)
            {
                // Aquí capturamos los errores lanzados desde Identity
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Error de registro", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado");
                // Para depuración temporal mostramos el mensaje real
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex.Message, ex.InnerException?.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message, object? errors = null)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Codigo = (int)statusCode,
                Mensaje = message,
                Errores = errors
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
