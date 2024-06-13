using System.Net.Mime;
using System.Reflection;
using System.Security.Authentication;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Philomela.Application.Exceptions;
using Philomela.Domain.Exceptions;

namespace Philomela.Api.Middlewares
{
    /// <summary>
    /// Middleware обработки исключений.
    /// </summary>
    internal class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/>.</param>
        /// <param name="logger"><see cref="ILogger{ErrorHandlingMiddleware}"/>.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            ArgumentNullException.ThrowIfNull(next);
            ArgumentNullException.ThrowIfNull(logger);

            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// InvokeAsync.
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/>.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Произошло исключение. Параметры запроса : Request status: {httpContext.Response.StatusCode};  Web method: {httpContext.Request.Method}; Path: {httpContext.Request.Path}");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = exception switch
            {
                TimeoutException _ => StatusCodes.Status504GatewayTimeout,
                AuthenticationException _ => StatusCodes.Status401Unauthorized,
                AppException _ => StatusCodes.Status400BadRequest,
                DomainException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            return httpContext.Response.WriteAsJsonAsync(
                new ErrorResponse
                {
                    Detail = exception.Message,
                    Status = httpContext.Response.StatusCode,
                    Instance = Assembly.GetEntryAssembly().GetName().Name,
                },
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                });
        }
    }
}
