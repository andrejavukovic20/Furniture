using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalonNamjestaja.Repository
{
    public class BonusCardRepository : IBonusCardRepository
    {
        private readonly FurnitureDbContext dbContext;

        public BonusCardRepository(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    

        public async Task<BonusCard> GetBonusCardByIdAsync(int id)
        {
            return await dbContext.BonusCards
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.BonusCardId == id);
        }

        public async Task<IReadOnlyList<BonusCard>> GetBonusCardsAsync()
        {
            return await dbContext.BonusCards
                .Include(p => p.Customer)
                .ToListAsync();
        }
        public async Task<BonusCard> AddAsync(BonusCard bonusCard)
        {
            bonusCard.BonusCardId = new Random().Next();
            await dbContext.BonusCards.AddAsync(bonusCard);
            await dbContext.SaveChangesAsync();
            return bonusCard;
        }
        public async Task<BonusCard> UpdateAsync(int id, BonusCard bonusCard)
        {
            var existingCard = await dbContext.BonusCards
             .FirstOrDefaultAsync(x => x.BonusCardId == id);
            if (existingCard == null)
            {
                return null;
            }

            existingCard.Type = bonusCard.Type;
            existingCard.Number = bonusCard.Number;
            existingCard.CustomerId = bonusCard.CustomerId;

            await dbContext.SaveChangesAsync();
            return existingCard;
        }

        public async Task<BonusCard> DeleteAsync(int id)
        {
            var existingCard = await dbContext.BonusCards.FirstOrDefaultAsync(x => x.BonusCardId == id);

            if (existingCard == null)
            {
                return null;
            }

            dbContext.BonusCards.Remove(existingCard);
            await dbContext.SaveChangesAsync();
            return existingCard;
        }
    }
}
