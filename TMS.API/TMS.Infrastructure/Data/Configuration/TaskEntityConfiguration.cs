using TMS.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TMS.Application.Entities.Task;

namespace TMS.Infrastructure.Data.Configuration
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(1000);
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.AssignedToUserId).IsRequired();
            builder.Property(t => t.CreatedUserId).IsRequired();
            builder.Property(t => t.TeamId).IsRequired();
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
