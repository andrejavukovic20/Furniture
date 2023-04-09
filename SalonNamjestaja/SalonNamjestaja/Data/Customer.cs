using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Data;

public partial class Customer 
{
    [Key]
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<BonusCard> BonusCards { get; } = new List<BonusCard>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual UserType User { get; set; } = null!;
}
