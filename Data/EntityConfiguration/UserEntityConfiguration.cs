using Domain.Core.Entities;
using Domain.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasMany(u => u.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            entity.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Serhii",
                    LastName = "Babenko",
                    Email = "sergebabenk@gmail.com",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jone",
                    LastName = "Doe",
                    Email = "jonedoe@gmail.com",
                    CreatedAt = DateTime.UtcNow
                }
    );
        }
    }
}
