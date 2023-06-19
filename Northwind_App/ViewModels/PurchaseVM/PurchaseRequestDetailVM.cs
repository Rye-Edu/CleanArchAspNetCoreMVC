using Microsoft.AspNetCore.Mvc;
using Northwind_App.Interfaces.Common;
using Northwind_App.ViewModels.ProductVM;
using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.ViewModels.PurchaseVM
{
    public class PurchaseRequestDetailVM:IEntity
    {
      //  [BindProperties(Name ="RequestId",SupportsGet = true)]
        public int? RequestId { get; set; }

        public int UserId { get; set; }

        public decimal UnitPrice  { get; set; }
        public int ProductID { get; set; }
        public int ItemPage { get; set; }
        public int Pages { get; set; }

        [DisplayName("Quantity in Units")]
   
        public int QuantityRequested { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateRequested { get; set; }

        public string? Status { get; set; }

      //  [NotMapped]
        public virtual ProductViewModel? Product { get; set; }


        public string? ProductName
        {
            get
            {
                return Product?.ProductName ?? null;
            }
            set {
                Product = new ProductViewModel();
                ProductName = value;
            }
        }
        public virtual ICollection<StorePurchase> StorePurchases { get; set; } = new List<StorePurchase>();

        public virtual User? User { get; set; }

        [DisplayName("Requester")]
        //[FromQuery]
        public string? FullName
        {
            get
            {
                return $" {User?.Employee?.FirstName} {User?.Employee?.LastName}" ?? string.Empty;
            }

        }

        public IList<PurchaseRequestDetailVM>? PagedItems { get; set; } = new List<PurchaseRequestDetailVM>();
        public IEnumerable<PurchaseRequestDetailVM> RequestList { get; set; } = null!;

        //public List<PurchaseRequestDetailVM>? PurchaseRequestDetailList { get; set; }
    }
}
