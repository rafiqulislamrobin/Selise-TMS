using TMS.Application.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMS.Infrastructure.Data.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(s => s.Id);


            builder
                .Property(s => s.Email)
                .HasMaxLength(100);

            builder
                .Property(s => s.Role)
                .HasMaxLength(50);

        }
    }

}
