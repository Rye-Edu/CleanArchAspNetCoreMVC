using System;
using System.Collections.Generic;

namespace Northwind_Core.Domain.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public int? EmployeeId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee? Employee { get; set; }
}
