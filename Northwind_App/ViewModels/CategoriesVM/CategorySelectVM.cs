using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Northwind_App.ViewModels.CategoriesVM
{
    public class CategorySelectVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryItemId { get; set; }
        public IEnumerable<SelectList> CategoryItem { get; set; } = null;
    }
}
