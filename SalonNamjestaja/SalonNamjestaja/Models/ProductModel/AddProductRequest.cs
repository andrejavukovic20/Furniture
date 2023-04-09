using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.ProductModel
{
    public class AddProductRequest
    {
        [Required]
        [MaxLength(40, ErrorMessage = "The name of the product can contain a maximum of 40 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [MaxLength(20, ErrorMessage = "The name of the color can contain a maximum of 20 characters.")]
        public string? Color { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
