using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Coba_Net.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Filter
{
    public class ValidateCookie : IActionFilter
    {
        private readonly Jwt _jwt;

        public ValidateCookie(Jwt jwt)
        {
            _jwt = jwt;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var userInfo = _jwt.ValidateToken(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Jwt")?.Value);
            if (userInfo == null)
            {
                await context.HttpContext.SignOutAsync();
                context.Result = new RedirectResult("/User/Login");
            }
            else
            {
                var claimsIdentity = _jwt.GetClaimsPrincipal(userInfo);
                await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsIdentity);
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    controller.ViewData["Email"] = userInfo.Email;
                    controller.ViewData["Name"] = userInfo.Name;
                    controller.ViewData["PpUrl"] = userInfo.PpUrl;
                    controller.ViewData["Role"] = userInfo.Role;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}