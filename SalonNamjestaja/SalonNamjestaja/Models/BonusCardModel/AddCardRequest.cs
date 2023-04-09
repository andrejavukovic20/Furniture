using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.BonusCardModel
{
    public class AddCardRequest
    {
        [Required]
        [MaxLength(30, ErrorMessage = "The name of the street can contain a maximum of 30 characters.")]
        public string Type { get; set; } = null!;

        [Required]
        public decimal Number { get; set; }

        [Required]
        public int CustomerId { get; set; }

    }
}
