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
        services.AddDbContext<ApplicationContext>(config =>
        {
            config.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
        });

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationContext>();

        services.AddScoped<IApplicationContext, ApplicationContext>();

        return services;
    }
}