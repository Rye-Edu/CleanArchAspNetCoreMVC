using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Approved By")]
        public string? Approver
        {
            get
            {
                return $"{UserApprover!.Employee!.FirstName} {UserApprover!.Employee!.LastName}" ?? string.Empty;
            }
        }

        [DisplayName("Requester")]
        public string? RequestedBy {
            get {
                return $"{PurchaseRequest!.User!.Employee!.FirstName} {PurchaseRequest!.User!.Employee!.LastName}" ?? string.Empty;
            }
        }

        public int TotalPage { get; set; }
        public IEnumerable<StorePurchaseVM> ApprovedList { get; set; } = null!;
    }
}
