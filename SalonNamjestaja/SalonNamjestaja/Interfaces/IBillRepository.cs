using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IBillRepository
    {
        Task<IReadOnlyList<Bill>> GetBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task<Bill> AddAsync(Bill bill);
        Task<Bill> UpdateAsync(int id, Bill bill);
        Task<Bill> DeleteAsync(int id);
    }
}
