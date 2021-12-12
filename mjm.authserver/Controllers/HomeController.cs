using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mjm.authserver.Models;
using mjm.authserver.Services;

namespace mjm.authserver.Controllers;

[Authorize(Roles = "authAdmin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;

    public HomeController(ILogger<HomeController> logger, IUserService userService)
    {
        this._logger = logger;
        this._userService = userService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var exists = await this._userService.AdminExists();
        if(exists)
            return this.View();

        return this.RedirectToAction("CreateAdminUser");
    }

    [AllowAnonymous]
    public IActionResult CreateAdminUser()
    {
        return this.View();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateAdminUser(AuthAdminUserModel model)
    {
        await this._userService.CreateAdminUser(model);
        return this.RedirectToAction("Index");
    }

    public IActionResult DashBoard()
    {
        return this.View();
    }

    public IActionResult Privacy()
    {
        return this.View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}