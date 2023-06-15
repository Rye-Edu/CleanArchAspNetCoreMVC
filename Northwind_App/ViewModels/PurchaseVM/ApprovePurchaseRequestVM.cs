using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.ViewModels.PurchaseVM
{
    public class ApprovePurchaseRequestVM
    {
        public int PurchaseID { get; set; }
        public int PurchaseRequestId { get; set; }
        public int UserApproverId { get; set; }
        public int ApprovedQuantity { get; set; }
        public DateTime DateApproved { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public virtual PurchaseRequestDetailVM? RequestDetail { get; set; }
        public virtual User? User { get; set; }
    }
}
