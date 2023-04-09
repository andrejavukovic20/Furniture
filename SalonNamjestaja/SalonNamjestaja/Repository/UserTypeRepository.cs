using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly FurnitureDbContext dbContext;

        public UserTypeRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<UserType> GetUserTypeByIdAsync(int id)
        {
            return await dbContext.UserTypes
              .FirstOrDefaultAsync(p => p.UserId == id);
        }

        public async Task<IReadOnlyList<UserType>> GetUserTypesAsync()
        {
            return await dbContext.UserTypes
               .ToListAsync();
        }

        public async Task<UserType> AddAsync(UserType userType)
        {
            userType.UserId = new Random().Next();
            await dbContext.UserTypes.AddAsync(userType);
            await dbContext.SaveChangesAsync();
            return userType;
        }

        public async Task<UserType> UpdateAsync(int id, UserType userType)
        {
            var existingUser = await dbContext.UserTypes
              .FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.UserRole = userType.UserRole;

            await dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<UserType> DeleteAsync(int id)
        {
            var existingUser = await dbContext.UserTypes.FirstOrDefaultAsync(x => x.UserId == id);

            if (existingUser == null)
            {
                return null;
            }

            dbContext.UserTypes.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
