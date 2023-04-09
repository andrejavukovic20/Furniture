using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FurnitureDbContext dbContext;

        public AddressRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       

       
        public async Task<Address> GetAddressByIdAsync(int id)
        {
            return await dbContext.Addresses.FindAsync(id);
        }

        public async Task<IReadOnlyList<Address>> GetAddressesAsync()
        {
            return await dbContext.Addresses.ToListAsync();
        }
        public async Task<Address> AddAsync(Address address)
        {
            address.AddressId = new Random().Next();
            await dbContext.Addresses.AddAsync(address);
            await dbContext.SaveChangesAsync();
            return address;
        }
        public async Task<Address> UpdateAsync(int id, Address address)
        {
            var existingAddress = await dbContext.Addresses
              .FirstOrDefaultAsync(x => x.AddressId == id);
            if (existingAddress == null)
            {
                return null;
            }

            existingAddress.Street = address.Street;
            existingAddress.Number = address.Number;
            existingAddress.City = address.City;
            existingAddress.Zip = address.Zip;
            existingAddress.State = address.State;

            await dbContext.SaveChangesAsync();
            return existingAddress;
        }
        public async Task<Address> DeleteAsync(int id)
        {
            var existingAddress = await dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == id);

            if (existingAddress == null)
            {
                return null;
            }

            dbContext.Addresses.Remove(existingAddress);
            await dbContext.SaveChangesAsync();
            return existingAddress;
        }

    }
}
