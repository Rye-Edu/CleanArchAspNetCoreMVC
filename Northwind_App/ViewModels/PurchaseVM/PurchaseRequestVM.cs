using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.ViewModels.ProductVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.ViewModels.PurchaseVM
{
    public class PurchaseRequestVM
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
      
        [DisplayName("Current Quantity (unit in stock)")]
        public int UnitsInStock { get; set; } 

        [DisplayName("Request Quantity")]
        public int UnitsInStockRequest { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public ProductViewModel? ProductDetail { get; set; }
        public List<PurchaseRequestVM> PurchaseRequestList { get; set; } = new();

    }
}
