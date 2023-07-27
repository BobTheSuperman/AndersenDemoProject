using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private UserRepository _userRepository;
        private BlogRepository _blogRepository;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IUserRepository Users
        {
            get
            {
                if(_userRepository == null)
                {
                    return new UserRepository(_applicationContext);
                }

                return _userRepository;
            }
        }

        public IBlogRepository Blogs
        {
            get
            {
                if (_blogRepository == null)
                {
                    return new BlogRepository(_applicationContext);
                }

                return _blogRepository;
            }
        }

        public void Dispose()
        {
            _applicationContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
