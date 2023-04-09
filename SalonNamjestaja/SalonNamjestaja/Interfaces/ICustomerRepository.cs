using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IReadOnlyList<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(int id, Customer customer);
        Task<Customer> DeleteAsync(int id);
    }
}
