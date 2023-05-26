using System;
using System.Collections.Generic;

namespace Product_CoreDomain.Products_Infrastructure.DataModels;

public partial class User
{
    public Guid UserId { get; set; }

    public int? EmployeeId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee? Employee { get; set; }
}
