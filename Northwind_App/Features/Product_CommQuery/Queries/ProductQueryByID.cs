using MediatR;
using Northwind_App.ViewModels.ProductVM;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductQueryByID:IRequest<ProductViewModel>
    {
        public int ProductID { get; set; }

        public ProductQueryByID(int productID)
        {
            ProductID = productID;
        }
    }
}
