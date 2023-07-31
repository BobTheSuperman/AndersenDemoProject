using Domain.Core.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BlogRepository : Repository<Blog, ApplicationContext>, IBlogRepository
    {
        public BlogRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Blog>> GetBlogsByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return await _context.Blogs.Where(b => ids.Any(id => id == b.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId)
        {
            return await _context.Blogs.Where(b => b.AuthorId == userId).ToListAsync();
        }
    }
}
