using System;
using System.Collections.Generic;

namespace SalonNamjestaja.Data;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
