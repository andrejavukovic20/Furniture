using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Data;

public partial class Order
{
    [Key] 
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public double TotalPrice { get; set; }

    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
