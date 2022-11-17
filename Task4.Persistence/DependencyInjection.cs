using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task4.Application.Interfaces;
using Task4.Domain;
using Task4.Persistence.Contexts;

namespace Task4.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                               == "Production"
            ? GetConnectionString()
            : configuration["ConnectionStrings:DefaultConnection"];

        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(connectionString);
        });

        services.AddIdentity<User, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IApplicationContext, ApplicationDbContext>();

        return services;
    }

    private static string GetConnectionString()
    {
        var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        connectionUrl = connectionUrl!.Replace("postgres://", string.Empty);
        var userPassSide = connectionUrl.Split("@")[0];
        var hostSide = connectionUrl.Split("@")[1];

        var user = userPassSide.Split(":")[0];
        var password = userPassSide.Split(":")[1];
        var host = hostSide.Split("/")[0];
        var database = hostSide.Split("/")[1].Split("?")[0];

        return $"Host={host};Database={database};Username={user};" +
               $"Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }
}