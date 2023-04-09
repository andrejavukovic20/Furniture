using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Models.BillModel
{
    public class UpdateBillRequest
    {
        [Required]
        public decimal Number { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "The name of the publisher can contain a maximum of 40 characters.")]
        public string Publisher { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal Vat { get; set; }
        [Required]
        public bool Paid { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
