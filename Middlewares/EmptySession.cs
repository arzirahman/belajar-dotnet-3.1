using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Coba_Net.Utils;
using Microsoft.Extensions.Configuration;
using System;

namespace Coba_Net.Middlewares
{
    public class EmptySession
    {
        private readonly RequestDelegate _next;
        private readonly Jwt _jwt;

        public EmptySession(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _jwt = new Jwt(configuration);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sessionCookie = context.Request.Cookies["session"];

            if (!string.IsNullOrEmpty(sessionCookie) && _jwt.ValidateToken(sessionCookie) != null)
            {
                context.Response.Redirect("/");
                return;
            }

            await _next(context);
        }
    }
}
