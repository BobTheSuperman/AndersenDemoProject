using Domain.Core.Entities;

namespace Domain.Core.DTO.Messages.Blogs
{
    public class BlogResponse : BaseResponse
    {
        public Blog Blog { get; set; }
    }
}
