using Northwind_App.Interfaces.IRepositories;
using Northwind_Core.Domain.Entities;

namespace Northwind_Infrastructure.Repositories
{
    public interface IStorePurchaseRepository:IEntity, IAsyncBaseRepository<StorePurchase>
    {
        // public Task
        public Task<IQueryable<StorePurchase>> GetStorePurchases();
    }
}