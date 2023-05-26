using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind_App.Interfaces.Common;
using Northwind_App.ViewModels.CategoriesVM;
using Northwind_App.ViewModels.SupplierVM;
using System.ComponentModel;

namespace Northwind_App.ViewModels.ProductVM
{
    public class ProductViewModel : ITEntity<ITemId>
    {
        public ITemId RowID { get; set; }

        private string? imageFileName;
        public int? ProductId { get; set; }

        [DisplayName("Name")]
        public string ProductName { get; set; } = null!;


        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }
        [DisplayName("Quantity Per Unit")]
        public string? QuantityPerUnit { get; set; }

        [DisplayName("Unit Price")]
        public decimal? UnitPrice { get; set; }


        [DisplayName("Units In Stock")]
        public short? UnitsInStock { get; set; }

        [DisplayName("Units On Order")]
        public short? UnitsOnOrder { get; set; }

        [DisplayName("Reorder Level")]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        [DisplayName("Image File")]
        public string? ImageFile {
            get {
                return imageFileName;
            }
            set {
                imageFileName = value;
            }
        }

        [DisplayName("Upload Image")]
        public IFormFile? FileData { get; set; }
        public virtual CategoryViewModel? Category { get; set; }
        public SelectList Categories { get; set; } = null!;
        public SelectList Suppliers { get; set; } = null!;
        public virtual SupplierViewModel? Supplier { get; set; }

       
        public ProductFilterVM ProductFilter { get; set; } = new();
        public IList<ProductViewModel>? PagedItems { get; set; } = new List<ProductViewModel>();
        public IEnumerable<ProductViewModel> ProductList { get; set; } = null!;

    }
}
