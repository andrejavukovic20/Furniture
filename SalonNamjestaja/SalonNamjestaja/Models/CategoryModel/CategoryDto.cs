using SalonNamjestaja.Data;

namespace SalonNamjestaja.Models.CategoryModel
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
