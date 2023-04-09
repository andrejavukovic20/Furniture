using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.AddressModel
{
    public class UpdateAddressRequest
    {
        [Required]
        [MaxLength(40, ErrorMessage = "The name of the street can contain a maximum of 40 characters.")]
        public string Street { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "The number of the street can contain a maximum of 20 characters.")]
        public string Number { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "The name of the city can contain a maximum of 20 characters.")]
        public string City { get; set; } = null!;

        public decimal Zip { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The name of the state can contain a maximum of 20 characters.")]
        public string State { get; set; } = null!;
    }
}
