using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Coba_Net.Utils;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Coba_Net.Middlewares
{
    public class EmptySession
    {
        private readonly RequestDelegate _next;

        public EmptySession(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwt = context.RequestServices.GetRequiredService<Jwt>();
            var sessionCookie = context.Request.Cookies["session"];
            if (!string.IsNullOrEmpty(sessionCookie) && jwt.ValidateToken(sessionCookie) != null)
            {
                context.Response.Redirect("/");
                return;
            }

            await _next(context);
        }
    }
}
