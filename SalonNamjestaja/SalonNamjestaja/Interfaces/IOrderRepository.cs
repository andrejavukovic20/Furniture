using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetOrders();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> AddAsync(Order order);
        Task<Order> UpdateAsync(int id, Order order);
        Task<Order> DeleteAsync(int id);
    }
}
