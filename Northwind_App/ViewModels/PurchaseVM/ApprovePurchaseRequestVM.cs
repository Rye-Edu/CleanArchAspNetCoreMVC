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
        public int Purchase_RequestID { get; set; }
        public int User_ApproverID { get; set; }
        public int ApprovedQuantity { get; set; }
        public DateTime DateApproved { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
    }
}
