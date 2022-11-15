using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Task4.Application.Common.Constants;
using Task4.Persistence.Contexts;

namespace Task4.Persistence.ContextDesignFactories;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var projectPath = GetSettingsPath();
        
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(configurationBuilder["ConnectionStrings:DefaultConnection"],
            b
                => b.MigrationsAssembly(NameProjectAssemblies.Persistence));

        return new ApplicationContext(optionsBuilder.Options);
    }

    private static string GetSettingsPath()
    {
        var currentProjectPath = AppDomain.CurrentDomain.BaseDirectory;
        var currentProjectName = Assembly.GetExecutingAssembly().GetName().Name!;
        var targetProjectName = currentProjectPath
            .Replace(currentProjectName, NameProjectAssemblies.Mvc);

        return targetProjectName;
    }
}