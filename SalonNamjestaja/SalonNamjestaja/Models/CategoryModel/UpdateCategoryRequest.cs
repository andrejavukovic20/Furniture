using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.CategoryModel
{
    public class UpdateCategoryRequest
    {

        [Required]
        [MaxLength(40, ErrorMessage = "The name of the product category can contain a maximum of 40 characters.")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
