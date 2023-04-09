using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IAddressRepository
    {
        Task<IReadOnlyList<Address>> GetAddressesAsync();
        Task<Address> GetAddressByIdAsync(int id);
        Task<Address> AddAsync(Address address);
        Task<Address> UpdateAsync(int id, Address address);
        Task<Address> DeleteAsync(int id);
    }
}
