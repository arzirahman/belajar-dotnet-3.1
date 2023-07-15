using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Coba_Net.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace Coba_Net.Middlewares
{
    public class Session
    {
        private readonly RequestDelegate _next;
        private readonly Jwt _jwt;

        public Session(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _jwt = new Jwt(configuration);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sessionCookie = context.Request.Cookies["session"];
            var claims = _jwt.ValidateToken(sessionCookie);
            if (string.IsNullOrEmpty(sessionCookie) || claims == null)
            {
                context.Response.Redirect("/User/Login");
                return;
            }
            var filterClaim = claims.Claims.Where(claim => claim.Type == ClaimTypes.Email);
            var token = _jwt.GenerateToken(new List<Claim>(filterClaim));
            context.Response.Cookies.Append("session", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                HttpOnly = true,
                Secure = false
            });
            await _next(context);
        }
    }
}
