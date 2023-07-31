namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        Task<bool> DoesExistByIdAsync(int id);

        Task CreateAsync(TEntity entity);

        Task CreateAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        Task DeleteById(int id);
    }
}
