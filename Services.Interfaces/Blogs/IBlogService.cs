using Domain.Core.DTO.Messages;
using Domain.Core.DTO.Messages.Blogs;
using Domain.Core.Models.Blogs;

namespace Services.Interfaces.Blogs
{
    public interface IBlogService
    {
        Task<BlogCreateResponse> CreateBlogAsync(BlogModel blog);

        Task<BlogResponse> GetBlog(int id);

        Task<BaseResponse> UpdateBlogAsync(int id, BlogModel blog);

        Task<BaseResponse> DeleteBlogAsync(int id);

        Task<BlogsResponse> GetBlogs(IEnumerable<int> ids);

        Task<BlogsResponse> GetBlogsByUserId(int userId);
    }
}
