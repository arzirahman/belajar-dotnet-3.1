using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Coba_Net.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Coba_Net.Middlewares
{
    public class Session
    {
        private readonly RequestDelegate _next;

        public Session(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwt = context.RequestServices.GetRequiredService<Jwt>();
            var sessionCookie = context.Request.Cookies["session"];
            var user = jwt.ValidateToken(sessionCookie);
            if (string.IsNullOrEmpty(sessionCookie) || user == null)
            {
                context.Response.Redirect("/User/Login");
                return;
            }
            if (context.Request.Path == "/") context.Response.Redirect("/Home");
            var newToken = jwt.GenerateToken(user);
            context.Response.Cookies.Append("session", newToken, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                HttpOnly = true,
                Secure = false
            });
            context.Items["Email"] = user.Email;
            context.Items["Name"] = user.Name;
            context.Items["PpUrl"] = user.PpUrl;
            await _next(context);
        }
    }
}
