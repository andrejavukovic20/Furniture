using SalonNamjestaja.Data;

namespace SalonNamjestaja.Models.UserTypeModel
{
    public class UserTypeDto
    {
        public int UserId { get; set; }

        public string UserRole { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
    }
}
