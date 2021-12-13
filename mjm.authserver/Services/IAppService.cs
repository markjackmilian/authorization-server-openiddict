using mjm.authserver.Classes;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace mjm.authserver.Services;

public interface IAppService
{
    Task<OpenIddictEntityFrameworkCoreApplication[]> GetApps();
}

class AppService : IAppService, IScoped
{
    private readonly IOpenIddictApplicationManager _applicationManager;

    public AppService(IOpenIddictApplicationManager applicationManager)
    {
        this._applicationManager = applicationManager;
    }

    public async Task<OpenIddictEntityFrameworkCoreApplication[]> GetApps()
    {
        var apps = await this._applicationManager.ListAsync().ToListAsync();
        return apps.Cast<OpenIddictEntityFrameworkCoreApplication>().ToArray();
    }
}