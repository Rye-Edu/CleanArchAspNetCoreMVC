using System;
using System.Collections.Generic;

namespace Northwind_Infrastructure.DataModels;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
