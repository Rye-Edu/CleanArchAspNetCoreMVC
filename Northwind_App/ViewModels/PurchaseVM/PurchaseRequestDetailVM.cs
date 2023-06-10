using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.ViewModels.PurchaseVM
{
    public class PurchaseRequestDetailVM
    {
        public int RequestId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int QuantityRequested { get; set; }

        public DateTime DateRequested { get; set; }

        public string? Status { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual ICollection<StorePurchase> StorePurchases { get; set; } = new List<StorePurchase>();

        public virtual User User { get; set; } = null!;
    }
}
