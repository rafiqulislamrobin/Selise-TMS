using TMS.Application.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMS.Infrastructure.Data.Configuration
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Name)
                .HasMaxLength(50);

            builder
                .Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }

}
