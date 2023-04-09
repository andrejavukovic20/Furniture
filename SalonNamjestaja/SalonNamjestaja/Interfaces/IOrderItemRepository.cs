using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IReadOnlyList<OrderItem>> GetOrderItems();
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<OrderItem> AddAsync(OrderItem orderItem);
        Task<OrderItem> UpdateAsync(int id, OrderItem orderItem);
        Task<OrderItem> DeleteAsync(int id);
    }
}
