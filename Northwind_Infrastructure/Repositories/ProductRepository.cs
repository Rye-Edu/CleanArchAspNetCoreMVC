using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Northwind_App.Interfaces.IRepositories;

using Northwind_Core.Domain.Entities;
using Northwind_App.Product_Feature.Queries;
using Northwind_Infrastructure.Data;

namespace Northwind_Infrastructure.Repositories
{
    public class ProductRepository : AsyncBaseRepository<Product>, IProductRepository
    {
        private readonly NorthwindContext _northwindContext;


        public ProductRepository(NorthwindContext northwindContext) : base(northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public async Task<IEnumerable<Product>> GetProductDetails()
        {
            var productDetails = _northwindContext.Products.Include(p => p.Category).Include(p => p.Supplier);
            return await Task.FromResult(productDetails);
        }

        public async Task<Product> GetSingleProduct(int Id)
        {

            var product = await _northwindContext.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(m => m.ProductId == Id);



            if (product != null) {
                return await Task.FromResult(product);
            }
            throw new Exception("No data");
        }

        public async Task<IEnumerable<Product>> GetSearchProduct(ProductListQuery search)
        {

            //var product = await _northwindContext.Products
            //.Include(p => p.Category)
            //.Include(p => p.Supplier).Where(m=> m.ProductName == search).ToListAsync();
            List<Product> found = new();
            //if (!search.IsNullOrEmpty()) {
            //    var product = await _northwindContext.Products.Include(categories => categories.Category).Include(suppliers => suppliers.Supplier)
            //       .Where(searched => searched.ProductName.Contains(search)).ToListAsync();
            //    if (product != null) {
            //        found = product;
            //    }
            //    return Task.FromResult(found).Result;
            throw new NotImplementedException();
        }

        // .Where(searched => search.All(p => searched.ProductName.Contains(p))).ToListAsync();
        //   throw new Exception("No data");

        //.Where(searchPhrase => EF.Functions.FreeText(searchPhrase.ProductName),"asdfaf");

        //.FirstOrDefaultAsync(m => m.ProductName == search);



        //if (product != null)
        //{

        /// }
    
      //  }
    }
}
