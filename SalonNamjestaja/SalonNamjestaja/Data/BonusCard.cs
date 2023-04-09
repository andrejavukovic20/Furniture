using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Data;

public partial class BonusCard
{
    [Key]
    public int BonusCardId { get; set; }

    public string Type { get; set; } = null!;

    public decimal Number { get; set; }

    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
