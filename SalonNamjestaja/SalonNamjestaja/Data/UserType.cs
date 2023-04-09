using System;
using System.Collections.Generic;

namespace SalonNamjestaja.Data;

public partial class UserType
{
    public int UserId { get; set; }

    public string UserRole { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}
