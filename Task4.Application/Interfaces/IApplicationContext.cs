using Microsoft.EntityFrameworkCore;
using Task4.Domain;

namespace Task4.Application.Interfaces;

public interface IApplicationContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}