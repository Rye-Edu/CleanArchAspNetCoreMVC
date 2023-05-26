using System;
using System.Collections.Generic;

namespace Product_CoreDomain.Products_Infrastructure.DataModels;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
