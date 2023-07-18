using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Coba_Net.Utils;
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
            if (context.Request.Path == "/") context.Response.Redirect("/Home");
            var filterClaims = claims.Claims.Where(claim => claim.Type == ClaimTypes.Email || claim.Type == ClaimTypes.Name);
            var token = _jwt.GenerateToken(new List<Claim>(filterClaims));
            context.Response.Cookies.Append("session", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                HttpOnly = true,
                Secure = false
            });
            var emailClaim = filterClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var nameClaim = filterClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            context.Items["Email"] = emailClaim;
            context.Items["Name"] = nameClaim;
            await _next(context);
        }
    }
}
