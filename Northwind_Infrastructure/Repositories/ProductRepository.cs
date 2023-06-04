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
                //if (selectedOption.IsNullOrEmpty() || selectedText.IsNullOrEmpty()) {
                //    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(suppliers => suppliers.Supplier)
                //        .Where(searchKeyword => searchKeyword.ProductName.Contains(searchPhrase.ToLower())).ToListAsync();
                //}
                else if (!selectedOption.IsNullOrEmpty() && selectedFilter.Contains("Supplier"))
                {
                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier)
                        .Where(option => option!.Supplier!.CompanyName.Contains(selectedFilter.ToLower())).ToListAsync();
                    //suppliers = await _northwindContext.Suppliers.Include(products => products.Products).Where(supplierName => supplierName.ContactName == searchPhrase).ToListAsync();
                    //    .Where(optionSelected => optionSelected?.Category == selectedOption).ToListAsync(); 
                }
                else if (!selectedOption.IsNullOrEmpty() && selectedOption == "Category")
                {

                    productList = await _northwindContext.Products.Include(categories => categories.Category).Include(supplier => supplier.Supplier)
                        .Where(option => option.ProductName.Contains(searchPhrase.ToLower()) && option!.Category!.CategoryName == selectedText).ToListAsync();

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
