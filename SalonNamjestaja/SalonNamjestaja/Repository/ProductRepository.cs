using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FurnitureDbContext dbContext;
        public ProductRepository(FurnitureDbContext dbContext)
        {
            this.dbContext= dbContext;
        }
      

        public async Task<Product> GetProductByIdAsync(int id)
        {
            
            return await dbContext.Products
                .Include(p=> p.Category)
                .FirstOrDefaultAsync(p => p.ProductId ==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
         
            product.ProductId = new Random().Next();
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            var existingProduct = await dbContext.Products
                .FirstOrDefaultAsync(x => x.ProductId == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Color = product.Color;
            existingProduct.Price = product.Price;
            existingProduct.Available= product.Available;
            existingProduct.CategoryId= product.CategoryId;

            await dbContext.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<Product> DeleteAsync(int id)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (existingProduct == null)
            {
                return null;
            }

            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
