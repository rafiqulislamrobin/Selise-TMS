using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Domain;
using TMS.Application.Entities;
using TMS.Infrastructure.Data.Configuration;
using Task = TMS.Application.Entities.Task;

namespace TMS.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Task> Tasks { get; set; }

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
        base.OnModelCreating(modelBuilder);
        new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
        new TeamEntityConfiguration().Configure(modelBuilder.Entity<Team>());
        new TaskEntityConfiguration().Configure(modelBuilder.Entity<Task>());
        modelBuilder.Entity<Team>();
    }
}
