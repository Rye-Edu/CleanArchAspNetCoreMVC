using System;
using System.Collections.Generic;

namespace Product_CoreDomain.Products_Infrastructure.DataModels;

public partial class ProductSalesFor1997
{
    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal? ProductSales { get; set; }
}
