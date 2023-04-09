using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<IReadOnlyList<ProductCategory>> GetProductCategoriesAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task<ProductCategory> AddAsync(ProductCategory productCategory);
        Task<ProductCategory> UpdateAsync(int id, ProductCategory productCategory);
        Task<ProductCategory> DeleteAsync(int id);
    }
}
