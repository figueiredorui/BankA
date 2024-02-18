using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Identity
{
    public class IdentityApiRestrictionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IdentityApiRestrictionsMiddleware> _logger;

        public IdentityApiRestrictionsMiddleware(RequestDelegate next, ILogger<IdentityApiRestrictionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower().StartsWith("/api/identity/register"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new ProblemDetails()
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Unauthorized",
                    Detail = "Access to requested resource is denied"
                });
            }
            else
            {
                await _next(context);
            }
        }
    }
}
