using Application.Common.Domain;
using Application.Entities;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries<IAudit>())
        {
            if (entity.State is EntityState.Added or EntityState.Modified)
            {
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.SetModifiedOn(DateTime.UtcNow);
                }

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.SetCreatedOn(DateTime.UtcNow);
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CustomerEntityConfiguration().Configure(modelBuilder.Entity<Customer>());
    }
}
