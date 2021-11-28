using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace mjmauth.core;

public static class Class1
{
    public static void addgino(this IServiceCollection services)
    {
        services.AddControllersWithViews();
            
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/account/login";
            });
    }
}