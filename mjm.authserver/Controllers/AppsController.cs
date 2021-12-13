using Microsoft.AspNetCore.Mvc;
using mjm.authserver.Services;

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
        return View();
    }
}