using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace mjmauth.core;

public static class StartupExtensions
{
    public static MjmAuthBuilder AddMjmAuth(this IServiceCollection services)
    {
        return new MjmAuthBuilder(services);
    }

    public static void RegisterMjmAuth(this WebApplication app)
    {
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
    }
}

public class MjmAuthBuilder
{
    private readonly IServiceCollection _serviceCollection;
    private readonly OpenIddictBuilder _openIdBuilder;

    protected internal MjmAuthBuilder(IServiceCollection serviceCollection)
    {
        this._serviceCollection = serviceCollection;
        this.AddViewAndAuth();
        this._openIdBuilder = this._serviceCollection.AddOpenIddict();
    }

    private void AddViewAndAuth()
    {
        
        this._serviceCollection.AddControllersWithViews().AddApplicationPart(this.GetType().Assembly).AddRazorRuntimeCompilation();
        this._serviceCollection.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new EmbeddedFileProvider(this.GetType().Assembly));
        });

        this._serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options => { options.LoginPath = "/account/login"; });
    }

    /// <summary>
    /// Add DbContext with OpenIdDict tables
    /// </summary>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public MjmAuthBuilder WithContext<T>(Action<DbContextOptionsBuilder> options) where T : DbContext
    {
        this._serviceCollection.AddDbContext<T>(builder =>
        {
            options.Invoke(builder);
            builder.UseOpenIddict();
        });

        return this;
    }


    public MjmAuthBuilder WithDefaultStartup()
    {
        // Register the OpenIddict core components.
        this._openIdBuilder.AddCore(options =>
            {
                // Configure OpenIddict to use the EF Core stores/models.
                options.UseEntityFrameworkCore()
                    .UseDbContext<DbContext>();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                options
                    .AllowClientCredentialsFlow()
                    .AllowAuthorizationCodeFlow()
                    .RequireProofKeyForCodeExchange()
                    .AllowPasswordFlow()
                    // .AcceptAnonymousClients()
                    .AllowRefreshTokenFlow();

                options
                    .SetTokenEndpointUris("/connect/token")
                    .SetAuthorizationEndpointUris("/connect/authorize")
                    .SetUserinfoEndpointUris("/connect/userinfo");

                // Encryption and signing of tokens
                options
                    .AddEphemeralEncryptionKey()
                    .AddEphemeralSigningKey()
                    .DisableAccessTokenEncryption();

                // Register scopes (permissions)
                options.RegisterScopes("api");

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options
                    .UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough();
            });
        return this;
    }
}