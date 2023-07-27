using Domain.Core.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class BlogRepository : Repository<Blog, ApplicationContext>, IBlogRepository
    {
        public BlogRepository(ApplicationContext context) : base(context)
        {
        }        
    }
}
