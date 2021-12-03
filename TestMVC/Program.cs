using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mjmauth.core;
using TestMVC.Areas.Identity.Data;
using TestMVC.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMjmAuth()
    .WithContext<TestMVCContext>(optionsBuilder =>
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=oiddict;User Id=sa;Password=123Stella!");
    })
    .WithDefaultStartup();
builder.Services.AddDefaultIdentity<TestMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
      .AddEntityFrameworkStores<TestMVCContext>();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.RegisterMjmAuth();
app.Run();

//var connectionString = builder.Configuration.GetConnectionString("TestMVCContextConnection"); 
//builder.Services.AddDbContext<TestMVCContext>(options => options.UseSqlServer(connectionString)); 
//builder.Services.AddDefaultIdentity<TestMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
//      .AddEntityFrameworkStores<TestMVCContext>();
//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();

//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();



//app.Run();
