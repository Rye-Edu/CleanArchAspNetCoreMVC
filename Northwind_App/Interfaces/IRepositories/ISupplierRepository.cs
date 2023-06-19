using Northwind_App.Interfaces.Common;
using Northwind_Core.Domain.Entities;

namespace Northwind_App.Interfaces.IRepositories
{
    public interface ISupplierRepository:IEntity, IAsyncBaseRepository<Supplier>
    {
        public Task<IEnumerable<Supplier>> SupplierNamesAndID();
    }
}
