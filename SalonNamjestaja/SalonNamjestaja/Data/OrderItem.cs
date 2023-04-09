using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Data;

public partial class OrderItem
{
    [Key, Column(Order = 0)]
    public int OrderItemId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    [Key, Column(Order=1)]
    public int OrderId { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
