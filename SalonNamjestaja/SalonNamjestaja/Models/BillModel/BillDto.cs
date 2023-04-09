using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Models.BillModel
{
    public class BillDto
    {
        
        public int BillId { get; set; }

        public decimal Number { get; set; }

        public string Publisher { get; set; } = null!;

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public decimal Vat { get; set; }

        public bool Paid { get; set; }

        public int OrderId { get; set; }
    }
}
