using Domain.Core.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        public Task<IEnumerable<Blog>> GetBlogsByIdsAsync(IEnumerable<int> ids);
        public Task<IEnumerable<Blog>> GetBlogsByUserIdAsync(int userId);
    }
}
