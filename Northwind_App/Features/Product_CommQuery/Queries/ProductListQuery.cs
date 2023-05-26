using MediatR;
using Northwind_App.ViewModels.ProductVM;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductListQuery:IRequest<ProductViewModel>
    {
        public ProductFilterVM? ProductFilter { get; set; }

        public ProductListQuery(ProductViewModel productViewModel)
        {
            ProductFilter = productViewModel.ProductFilter;
        }
    }
}
