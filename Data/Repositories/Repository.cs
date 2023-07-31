using Domain.Core.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);
        }

        public async Task CreateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await _entities.AddRangeAsync(entities);
        }

        public async Task DeleteById(int id)
        {
            var entity = await _entities.FirstOrDefaultAsync(entity => entity.Id == id);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public async Task<bool> DoesExistByIdAsync(int id)
        {
            return await _entities.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.AsNoTracking().ToListAsync<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
        }
    }
}
