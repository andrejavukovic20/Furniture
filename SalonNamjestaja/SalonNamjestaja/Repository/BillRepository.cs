using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;

namespace SalonNamjestaja.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly FurnitureDbContext dbContext;

        public BillRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    
        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await dbContext.Bills
                .FirstOrDefaultAsync(p => p.BillId == id);
        }

        public async Task<IReadOnlyList<Bill>> GetBillsAsync()
        {
            return await dbContext.Bills
                .ToListAsync();
        }
        public async Task<Bill> AddAsync(Bill bill)
        {
            bill.BillId = new Random().Next();
            await dbContext.Bills.AddAsync(bill);
            await dbContext.SaveChangesAsync();
            return bill;
        }


        public async Task<Bill> UpdateAsync(int id, Bill bill)
        {
            var existingBill = await dbContext.Bills
               .FirstOrDefaultAsync(x => x.BillId == id);
            if (existingBill == null)
            {
                return null;
            }

            existingBill.Number = bill.Number;
            existingBill.Publisher = bill.Publisher;
            existingBill.Date = bill.Date;
            existingBill.Amount = bill.Amount;
            existingBill.Vat = bill.Vat;
            existingBill.Paid = bill.Paid;
            existingBill.OrderId = bill.OrderId;

            await dbContext.SaveChangesAsync();
            return existingBill;
        }
        public async Task<Bill> DeleteAsync(int id)
        {
            var existingBill = await dbContext.Bills.FirstOrDefaultAsync(x => x.BillId == id);

            if (existingBill == null)
            {
                return null;
            }

            dbContext.Bills.Remove(existingBill);
            await dbContext.SaveChangesAsync();
            return existingBill;
        }

    }
}
