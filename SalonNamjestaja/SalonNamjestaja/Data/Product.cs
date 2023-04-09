using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Color { get; set; }

    public decimal Price { get; set; }

    public bool Available { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
