using System;
using System.Collections.Generic;

namespace Northwind_Infrastructure.DataModels;

public partial class Address
{
    public Guid AddressId { get; set; }

    public string AddressName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string Country { get; set; } = null!;
}
