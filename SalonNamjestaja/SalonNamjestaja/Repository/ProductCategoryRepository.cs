using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly FurnitureDbContext dbContext;

        public ProductCategoryRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

     

        public async Task<IReadOnlyList<Data.ProductCategory>> GetProductCategoriesAsync()
        {
            return await dbContext.ProductCategories.ToListAsync();
        }

        public async Task<Data.ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await dbContext.ProductCategories.FindAsync(id);
        }

        public async Task<ProductCategory> AddAsync(ProductCategory productCategory)
        {
            productCategory.CategoryId = new Random().Next();
            await dbContext.ProductCategories.AddAsync(productCategory);
            await dbContext.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> UpdateAsync(int id, ProductCategory productCategory)
        {
            var existingCategory = await dbContext.ProductCategories
             .FirstOrDefaultAsync(x => x.CategoryId == id);
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = productCategory.Name;

            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
        public async Task<ProductCategory> DeleteAsync(int id)
        {
            var existingCategory = await dbContext.ProductCategories.FirstOrDefaultAsync(x => x.CategoryId == id);

            if (existingCategory == null)
            {
                return null;
            }

            dbContext.ProductCategories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
