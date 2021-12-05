using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mjmauth.core;
using WebXample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMjmAuth()
    .WithContext<AuthContext>(optionsBuilder =>
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=mjmauth;User Id=sa;Password=123Stella!");
    })
    .WithDefaultStartup();

var app = builder.Build();
app.RegisterMjmAuth();
app.Run();
