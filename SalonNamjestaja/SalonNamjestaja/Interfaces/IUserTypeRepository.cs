using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IUserTypeRepository
    {
        Task<IReadOnlyList<UserType>> GetUserTypesAsync();
        Task<UserType> GetUserTypeByIdAsync(int id);
        Task<UserType> AddAsync(UserType userType);
        Task<UserType> UpdateAsync(int id, UserType userType);
        Task<UserType> DeleteAsync(int id);
    }
}
