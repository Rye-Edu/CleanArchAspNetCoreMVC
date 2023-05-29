using MediatR;
using Northwind_App.ViewModels.ProductVM;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductListQuery:IRequest<ProductFilterVM>
    {
        public ProductFilterVM? ProductFilter { get; set; }

        public ProductListQuery(ProductFilterVM productViewModel)
        {
            ProductFilter = productViewModel;
        }
    }
}
