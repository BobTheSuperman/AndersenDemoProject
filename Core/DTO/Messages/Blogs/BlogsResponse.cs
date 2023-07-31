using Domain.Core.Entities;

namespace Domain.Core.DTO.Messages.Blogs
{
    public class BlogsResponse : BaseResponse
    {
        public IEnumerable<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
