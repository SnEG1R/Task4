using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Interfaces;
using Task4.Domain;

namespace Task4.Persistence.Contexts;

public class ApplicationContext : IdentityDbContext<User>, IApplicationContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
    }
}