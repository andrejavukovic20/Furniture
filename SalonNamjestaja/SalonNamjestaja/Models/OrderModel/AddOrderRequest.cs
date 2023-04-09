using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Models.OrderModel
{
    public class AddOrderRequest
    {

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The status can contain a maximum of 20 characters.")]
        public string Status { get; set; } = null!;

        [Required]
        public string PaymentMethod { get; set; } = null!;

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
    }
}
