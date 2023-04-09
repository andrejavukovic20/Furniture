using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FurnitureDbContext dbContext;

        public OrderRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

     

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await dbContext.Orders
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p =>p.OrderId == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrders()
        {
            return await dbContext.Orders
                .Include(p => p.Customer)
                .ToListAsync();
        }
        public async Task<Order> AddAsync(Order order)
        {
            order.OrderId = new Random().Next();
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(int id, Order order)
        {
            var existingOrder = await dbContext.Orders
               .FirstOrDefaultAsync(x => x.OrderId == id);
            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.DeliveryDate = order.DeliveryDate;
            existingOrder.Status = order.Status;
            existingOrder.PaymentMethod = order.PaymentMethod;
            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.CustomerId = order.CustomerId;

            await dbContext.SaveChangesAsync();
            return existingOrder;
        }
        public async Task<Order> DeleteAsync(int id)
        {
            var existingOrder = await dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            if (existingOrder == null)
            {
                return null;
            }

            dbContext.Orders.Remove(existingOrder);
            await dbContext.SaveChangesAsync();
            return existingOrder;
        }
    }
}
