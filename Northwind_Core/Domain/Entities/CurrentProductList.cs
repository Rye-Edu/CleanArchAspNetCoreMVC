using System;
using System.Collections.Generic;

namespace Northwind_Core.Domain.Entities;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
