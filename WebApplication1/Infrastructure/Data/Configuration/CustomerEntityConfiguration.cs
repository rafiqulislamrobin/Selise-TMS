using Application.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Name)
                .HasMaxLength(50);

            builder
                .Property(s => s.Remarks)
                .HasMaxLength(500);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }

}
