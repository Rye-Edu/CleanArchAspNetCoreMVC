using Northwind_Core.Domain.Entities;

namespace Northwind_App.Interfaces.IRepositories
{
    public interface ICategoryRepository:IEntity, IAsyncBaseRepository<Category>
    {
        public Task<IEnumerable<Category>> CategoryNames();
    }
}
