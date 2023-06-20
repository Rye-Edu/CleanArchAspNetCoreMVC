using MediatR;
using Northwind_App.ViewModels.ProductVM;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductListQuery:IRequest<ProductViewModel>
    {

        public int ItemPage { get; set; }
        public int TotalPage { get; set; }
        public ProductFilterVM? ProductFilter { get; set; }

        public ProductListQuery(int itemPage, ProductViewModel productViewModel)
        {
            ProductFilter = productViewModel.ProductFilter;
            ItemPage = itemPage;
        }
    }
}
