using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4.Application;
using Task4.Application.Common.Mappings;
using Task4.Application.Interfaces;
using Task4.MVC.Filters;
using Task4.Persistence;
using Task4.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationContext).Assembly));
});

builder.Services.AddScoped<UserValidationAttribute>();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Login/Index";
});

builder.WebHost.ConfigureKestrel(config =>
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
    {
        config.Listen(IPAddress.Any, Convert.ToInt32(
            Environment.GetEnvironmentVariable("PORT")));   
    }
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();