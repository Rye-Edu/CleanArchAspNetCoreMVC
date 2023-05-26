using MediatR;
using Northwind_App.ViewModels.ProductVM;

namespace Northwind_App.Product_Feature.Commands
{
    public class UpdateProduct:IRequest<ProductViewModel>
    {
        public int? ProductID { get; set; }
        public ProductViewModel? Product { get; set; }
        public UpdateProduct(int? productID, ProductViewModel? product)
        {
            ProductID = productID;
            Product = product;
        }
    }
}
