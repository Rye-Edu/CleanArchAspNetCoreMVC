namespace Northwind_App.Interfaces.IRepositories
{
    public interface IAsyncBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIDAsync(int id);
        Task<IEnumerable<TEntity>> GetListEntitiesAsync();
        Task<TEntity> AddEntityAsync(TEntity entity);
        Task<TEntity> UpdateEntityAsync(TEntity entity);
        Task<TEntity> DeleteEntityAsync(int entity);
        Task SaveDB();
    }
}
