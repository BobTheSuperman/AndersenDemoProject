namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetByIdAsync(long id);

        Task<bool> DoesExistByIdAsync(long id);

        Task CreateAsync(TEntity entity);

        Task CreateAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        Task DeleteById(long id);
    }
}
