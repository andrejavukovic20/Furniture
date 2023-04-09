using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Models.BonusCardModel
{
    public class BonusCardDto
    {
        [Key]
        public int BonusCardId { get; set; }

        public string Type { get; set; } = null!;

        public decimal Number { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
