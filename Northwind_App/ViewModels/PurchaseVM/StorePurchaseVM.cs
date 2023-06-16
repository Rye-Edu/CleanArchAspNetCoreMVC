using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.ViewModels.PurchaseVM
{
    public class StorePurchaseVM
    {
        public int PurchaseId { get; set; }

        public int PurchaseRequestId { get; set; }

        public int UserApproverId { get; set; }

        public int ApprovedQuantity { get; set; }

        public DateTime DateApproved { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual PurchaseRequest? PurchaseRequest { get; set; }

        public virtual User? UserApprover { get; set; }
    }
}
