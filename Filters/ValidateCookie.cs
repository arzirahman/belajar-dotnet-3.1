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
            var jwt = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Jwt")?.Value;
            var userInfo = jwt != null ? _jwt.ValidateToken(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Jwt")?.Value) : null;
            if (userInfo == null)
            {
                await context.HttpContext.SignOutAsync();
                var returnUrl = context.HttpContext.Request.QueryString.ToString();
                context.Result = new RedirectResult($"/User/Login?ReturnUrl={returnUrl}");
            }
            else
            {
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    controller.ViewData["UserId"] = userInfo.Id.ToString();
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