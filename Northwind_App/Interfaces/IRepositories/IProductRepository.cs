using Northwind_App.Product_Feature.Queries;
using Northwind_Core.Domain.Entities;

namespace Northwind_App.Interfaces.IRepositories
{
    public interface IProductRepository:IEntity, IAsyncBaseRepository<Product>
    {
        public Task<IEnumerable<Product>> GetProductDetails();

        public Task<Product> GetSingleProduct(int id);
        public Task<IEnumerable<Product>> GetSearchProduct(ProductListQuery search);
    }
}
