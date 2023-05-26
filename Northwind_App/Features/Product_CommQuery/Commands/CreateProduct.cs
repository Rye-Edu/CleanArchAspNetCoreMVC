using MediatR;
using Northwind_App.ViewModels.ProductVM;

namespace Northwind_App.Features.Product_Feature.Commands
{
    public class CreateProduct:IRequest<ProductViewModel>
    {
    
        public ProductViewModel? Product { get; set; }
     
        public CreateProduct(ProductViewModel? product)
        {
            Product = product;
        }
    }
    
}
