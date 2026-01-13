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
                _logger.LogError(ex, "Unhandle Exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                TraceId = context.TraceIdentifier
            };

            switch (e)
            {
                // DB constraint violation
                case DbUpdateException: 
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = e.Message;
                    break;

                // Authentication problem
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = e.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "An unexpected error occured";
                    break;
            }

            if(_env.IsDevelopment())
            {
                response.Details = e.ToString();
            }

            var json = JsonSerializer.Serialize(response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            await context.Response.WriteAsync(json);
        }

        public class ErrorResponse
        {
            public string Message { get; set; } = null!;
            public string? Details { get; set; }
            public string TraceId { get; set; } = null!;
        }
    }
}
