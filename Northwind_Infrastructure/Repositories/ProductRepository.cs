using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Northwind_App.Interfaces.IRepositories;

using Northwind_Core.Domain.Entities;
using Northwind_App.Product_Feature.Queries;
using Northwind_Infrastructure.Data;
using Northwind_App.Product_CommQuery.Queries;

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

        public async Task<IEnumerable<Product>> GetSearchProduct(ProductSearch search)
        {
            string searchPhrase = search?.ProductFilters?.SearchPhrase?.ToString() ?? string.Empty;
            string selectedOption = search?.ProductFilters?.SelectedOption?.ToString() ?? string.Empty;
            string selectedText = search?.ProductFilters?.SelectedText?.ToString() ?? string.Empty;
            var x = search.ProductFilters.ToString();
            var productList = new List<Product>();
            
            if (!searchPhrase.IsNullOrEmpty())
            {

                if (!selectedOption.IsNullOrEmpty() && selectedOption == "Supplier") {
                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier)
                        .Where(option => option.ProductName.Contains(searchPhrase) && option.Supplier.CompanyName == selectedText).ToListAsync();
                       
                    //    .Where(optionSelected => optionSelected?.Category == selectedOption).ToListAsync(); 
                }
                else if (!selectedOption.IsNullOrEmpty() && selectedOption == "Category")
                {

                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier)
                        .Where(option => option.ProductName.Contains(searchPhrase) && option.Category.CategoryName == selectedText).ToListAsync();

                }


            }
                return Task.FromResult(productList).Result;
                /*throw new NotImplementedException()*/;
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
