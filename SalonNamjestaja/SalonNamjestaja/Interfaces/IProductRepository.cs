using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(int id, Product product);
        Task<Product> DeleteAsync(int id);
        
    }
}
