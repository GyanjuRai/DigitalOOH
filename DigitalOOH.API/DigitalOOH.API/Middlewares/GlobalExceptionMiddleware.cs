using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace DigitalOOH.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IHostEnvironment env
        )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"==================================> Unhandle Exception: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (ex)
            {
                case ValidationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Type = "ValidationError";
                    response.Message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Type = "UnauthorizedError";
                    response.Message = ex.Message;
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response.Type = "ConflictError";
                    response.Message = ex.Message;
                    break;

                case NotSupportedException:
                    context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                    response.Type = "UnsupportedMediaTypeError";
                    response.Message = ex.Message;
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Type = "NotFoundError";
                    response.Message = ex.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Type = "ServerError";
                    response.Message = "Something went wrong";
                    break;
            }

            if(_env.IsDevelopment())
            {
                response.Details = ex.ToString();
            }

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions 
                                                            { 
                                                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                                                            }
            );

            await context.Response.WriteAsync(json);
        }

        public class ErrorResponse
        {
            public string Type { get; set; } = null!;
            public string Message { get; set; } = null!;
            public string? Details { get; set; }
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }
    }
}
