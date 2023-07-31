using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration
{
    public class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> entity)
        {
            entity.HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .HasPrincipalKey(u => u.Id);

            entity.HasData(
                new Blog
                {
                    Id = 1,
                    Title = "First test blog",
                    Text = "About something",
                    AuthorId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Blog
                {
                    Id = 2,
                    Title = "Second test blog",
                    Text = "About first test blog",
                    AuthorId = 1,
                    CreatedAt = DateTime.UtcNow
                }
                );
        }
    }
}
