using System;
using System.Collections.Generic;

namespace Northwind_Core.Domain.Entities;

public partial class StorePurchase
{
    public int PurchaseId { get; set; }

    public int PurchaseRequestId { get; set; }

    public int UserApproverId { get; set; }

    public int ApprovedQuantity { get; set; }

    public DateTime DateApproved { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual PurchaseRequest PurchaseRequest { get; set; } = null!;

    public virtual User UserApprover { get; set; } = null!;
}
