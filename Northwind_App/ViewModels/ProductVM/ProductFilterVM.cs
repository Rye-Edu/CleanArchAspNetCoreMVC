using Microsoft.AspNetCore.Mvc.Rendering;
//using NuGet.Protocol.Core.Types;
using System.ComponentModel;

namespace Northwind_App.ViewModels.ProductVM
{
    public class ProductFilterVM
    {

        public IEnumerable<ProductViewModel> ProductSearchList { get; set; } = null!;
        public string? SelectedText  { get; set; }
        public string TextID { get; private set; }
        // public ProductSeachPhraseEnum seachPhraseEnum { get; set; }
        [DisplayName("search value")]
        public string? SearchPhrase { get; set; }

        public bool? DiscontinuedItem { get; set; }


        public string? SelectedOption { get; set; }
        public SelectList SearchFilters
        {
            get
            {

                return new SelectList(Enum.GetValues(typeof(ProductSeachPhraseEnum)).Cast<ProductSeachPhraseEnum>().Select(item => new SelectListItem
                {
                    Text = item.ToString(),
                    Value = ((int)item).ToString()
                }), "Text", "Text", "Text");
            }
          
        }

    }
}
