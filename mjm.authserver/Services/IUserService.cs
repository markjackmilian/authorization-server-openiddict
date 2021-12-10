using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using mjm.authserver.Classes;
using mjm.authserver.Data;
using mjm.authserver.Models;

namespace mjm.authserver.Services;

public interface IUserService
{
    /// <summary>
    /// Check if exists at least one user with role authadmin
    /// </summary>
    /// <returns></returns>
    Task<bool> AdminExists();

    /// <summary>
    /// Create a admin user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task CreateAdminUser(AuthAdminUserModel model);
}

class UserService : IUserService, IScoped
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        this._context = context;
        this._roleManager = roleManager;
        this._userManager = userManager;
    }

    public async Task<bool> AdminExists()
    {
        //var result = await this._roleManager.CreateAsync(new IdentityRole("authAdmin"));
        var authAdminUsers = await this._userManager.GetUsersInRoleAsync("authAdmin");
        return authAdminUsers.Any();
    }

    public async Task CreateAdminUser(AuthAdminUserModel model)
    {
        await this._userManager.CreateAsync(new IdentityUser(model.UserName),model.Password);
        var user = await this._userManager.FindByNameAsync(model.UserName);
        await this._userManager.AddToRoleAsync(user, "authAdmin");
    }
}