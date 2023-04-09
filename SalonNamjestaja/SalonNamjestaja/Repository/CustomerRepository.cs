using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FurnitureDbContext dbContext;

        public CustomerRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

   

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await dbContext.Customers
                .Include(p => p.Address)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.CustomerId ==id);
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersAsync()
        {
            return await dbContext.Customers
                .Include(p => p.Address)
                .Include(p => p.User)
                .ToListAsync();
        }
        public async Task<Customer> AddAsync(Customer customer)
        {
            customer.CustomerId = new Random().Next();
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return customer;
        }

       
        public async Task<Customer> UpdateAsync(int id, Customer customer)
        {
            var existingCustomer = await dbContext.Customers
               .FirstOrDefaultAsync(x => x.CustomerId == id);
            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.Username = customer.Username;
            existingCustomer.Password = customer.Password;
            existingCustomer.AddressId = customer.AddressId;
            existingCustomer.UserId = customer.UserId;

            await dbContext.SaveChangesAsync();
            return existingCustomer;
        }
        public async Task<Customer> DeleteAsync(int id)
        {
            var existingCustomer = await dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            if (existingCustomer == null)
            {
                return null;
            }

            dbContext.Customers.Remove(existingCustomer);
            await dbContext.SaveChangesAsync();
            return existingCustomer;
        }
    }
}
