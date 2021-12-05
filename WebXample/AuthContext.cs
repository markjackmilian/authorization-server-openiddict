using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebXample;

public class AuthContext : IdentityDbContext<IdentityUser>
{
    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlServer("Server=localhost;Database=mjmauth;User Id=sa;Password=123Stella!");
        base.OnConfiguring(optionsBuilder);
    }
}