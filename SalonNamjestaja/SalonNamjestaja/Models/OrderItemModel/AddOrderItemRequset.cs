using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Models.OrderItemModel
{
    public class AddOrderItemRequset
    {
        [Key, Column(Order = 0)]
        [Required]

        [MaxLength(40, ErrorMessage = "The name of product can contain a maximum of 40 characters.")]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

    

    }
}
