using Northwind_App.ViewModels.ProductVM;
//using Product_CoreDomain.Products_Infrastructure.DataModels;
using System.ComponentModel;

namespace Northwind_App.ViewModels.CategoriesVM
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [DisplayName("Catgory")]
        public string? CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public byte[]? Picture { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; } = new List<ProductViewModel>();
    }
}
