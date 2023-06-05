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

            string searchPhrase = search?.ProductFilter?.SearchPhrase?.ToString() ?? string.Empty;
            string selectedOption = search?.ProductFilter?.SelectedOption?.ToString() ?? string.Empty;
            string selectedText = search?.ProductFilter?.SelectedText?.ToString() ?? string.Empty;
            string selectedFilter = search?.ProductFilter?.SelectedFilter?.ToString() ?? string.Empty;
            var x = search?.ProductFilter?.ToString();
            var productList = new List<Product>();
            var suppliers = new List<Supplier>();
            if (!searchPhrase.IsNullOrEmpty())
            {
                if (selectedFilter == "Supplier") {
                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier)
                                .Where(supplierName => supplierName!.Supplier!.CompanyName.Contains(searchPhrase.ToLower())).ToListAsync();
                }
                else if (selectedFilter == "Product") {
                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier).
                        Where(productName => productName.ProductName.Contains(searchPhrase)).ToListAsync();
                }

                else if (selectedFilter == "Category")
                {
                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier).
                        Where(categoryName => categoryName!.Category!.CategoryName.Contains(searchPhrase)).ToListAsync();
                }
                else if (selectedFilter.IsNullOrEmpty()) {
                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier).
                       Where(productName => productName.ProductName.Contains(searchPhrase) ||
                       productName!.Supplier!.ContactName!.Contains(searchPhrase) ||
                       productName!.Category!.CategoryName.Contains(searchPhrase)).ToListAsync();
                }
             


            }
            return Task.FromResult(productList).Result;
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
