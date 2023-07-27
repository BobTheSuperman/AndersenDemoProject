using Domain.Core.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User, ApplicationContext>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
