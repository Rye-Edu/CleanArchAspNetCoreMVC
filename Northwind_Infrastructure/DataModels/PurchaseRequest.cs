﻿using System;
using System.Collections.Generic;

namespace Northwind_Infrastructure.DataModels;

public partial class PurchaseRequest
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
