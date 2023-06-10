using System;
using System.Collections.Generic;

namespace Northwind_Core.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int EmployeeId { get; set; }

    public string UserName { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();

    public virtual ICollection<StorePurchase> StorePurchases { get; set; } = new List<StorePurchase>();
}
