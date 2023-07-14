using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


public class Session
{
    private readonly RequestDelegate _next;

    public Session(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Mengecek apakah cookie session ada
        var sessionCookie = context.Request.Cookies["session"];

        if (string.IsNullOrEmpty(sessionCookie))
        {
            // Cookie session tidak ada, redirect ke halaman login
            context.Response.Redirect("/User/Login");
            return;
        }

        // Cookie session ada, lanjutkan ke middleware berikutnya
        await _next(context);
    }
}
