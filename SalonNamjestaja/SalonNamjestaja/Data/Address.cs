using System;
using System.Collections.Generic;

namespace SalonNamjestaja.Data;

public partial class Address
{
    public int AddressId { get; set; }

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string City { get; set; } = null!;

    public decimal Zip { get; set; }

    public string State { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}
