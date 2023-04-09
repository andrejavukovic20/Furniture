using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly FurnitureDbContext dbContext;

        public OrderItemRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await dbContext.OrderItems
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.OrderItemId == id);
        }

        public async Task<IReadOnlyList<OrderItem>> GetOrderItems()
        {
            return await dbContext.OrderItems
                .Include(p => p.Order)
                .Include(p => p.Product)
                .ToListAsync();
        }
        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            orderItem.OrderItemId = new Random().Next();
            await dbContext.OrderItems.AddAsync(orderItem);
            await dbContext.SaveChangesAsync();
            return orderItem;
        }

    
        public async Task<OrderItem> UpdateAsync(int id, OrderItem orderItem)
        {
            var existingOrderItem = await dbContext.OrderItems
               .FirstOrDefaultAsync(x => x.OrderItemId == id);
            if (existingOrderItem == null)
            {
                return null;
            }

            existingOrderItem.ProductName = orderItem.ProductName;
            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.Price = orderItem.Price;
            existingOrderItem.Price = orderItem.Price;
            existingOrderItem.OrderId = orderItem.OrderId;
            existingOrderItem.ProductId = orderItem.ProductId;

            await dbContext.SaveChangesAsync();
            return existingOrderItem;
        }
        public async Task<OrderItem> DeleteAsync(int id)
        {
            var existingOrderItem = await dbContext.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == id);

            if (existingOrderItem == null)
            {
                return null;
            }

            dbContext.OrderItems.Remove(existingOrderItem);
            await dbContext.SaveChangesAsync();
            return existingOrderItem;
        }
    }
}
