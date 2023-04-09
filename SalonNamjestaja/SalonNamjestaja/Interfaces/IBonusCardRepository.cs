using SalonNamjestaja.Data;

namespace SalonNamjestaja.Interfaces
{
    public interface IBonusCardRepository
    {
        Task<IReadOnlyList<BonusCard>> GetBonusCardsAsync();
        Task<BonusCard> GetBonusCardByIdAsync(int id);
        Task<BonusCard> AddAsync(BonusCard bonusCard);
        Task<BonusCard> UpdateAsync(int id, BonusCard bonusCard);
        Task<BonusCard> DeleteAsync(int id);
    }
}
