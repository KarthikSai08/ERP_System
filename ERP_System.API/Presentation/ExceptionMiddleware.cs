using ERP_System.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ERP_System.API.Presentation
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (ConflictException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                
                object response = ex.Errors.Any()
                        ? new { errors = ex.Errors }
                        : new { errors = (object)new { General = new { error = ex.Message } } };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (InsufficientStockException ex)
            {
                context.Response.StatusCode = 422;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "An Unexpected error occurred" });
            }

        }
    }
}
