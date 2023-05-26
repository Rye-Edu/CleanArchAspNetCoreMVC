using Northwind_App.Interfaces.IRepositories;
using System;
using System.Collections.Generic;

namespace Product_CoreDomain.Products_Infrastructure.DataModels;

public partial class Category:IEntity
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
