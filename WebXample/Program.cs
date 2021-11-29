using Microsoft.EntityFrameworkCore;
using mjmauth.core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMjmAuth()
    .WithContext<DbContext>(optionsBuilder =>
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=oiddict;User Id=sa;Password=123Stella!");
    })
    .WithDefaultStartup();

var app = builder.Build();
app.RegisterMjmAuth();
app.Run();
