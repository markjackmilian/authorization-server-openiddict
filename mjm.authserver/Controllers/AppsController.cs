using Microsoft.AspNetCore.Mvc;
using mjm.authserver.Services;
using OpenIddict.EntityFrameworkCore.Models;

namespace mjm.authserver.Controllers;

public class AppsController : Controller
{
    private readonly IAppService _appService;

    public AppsController(IAppService appService)
    {
        this._appService = appService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var apps = await this._appService.GetApps();
        return View(apps);
    }

    public async Task<IActionResult> Edit(string id = null)
    {
        var app = id == null ? new OpenIddictEntityFrameworkCoreApplication() : await this._appService.GetApp(id);
        return this.View(app);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(OpenIddictEntityFrameworkCoreApplication application)
    {
        var app = new OpenIddictEntityFrameworkCoreApplication();
        return this.View(app);
    }
}