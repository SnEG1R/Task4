using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task4.Persistence.Contexts;

namespace Task4.Persistence.Initializers;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<ApplicationContext>();
        
        context.Database.EnsureCreated();
    }
}