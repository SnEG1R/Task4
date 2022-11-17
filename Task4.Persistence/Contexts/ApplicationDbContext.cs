using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Interfaces;
using Task4.Domain;

namespace Task4.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<long>, long>,
    IApplicationContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}