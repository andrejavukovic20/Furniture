using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.UserTypeModel
{
    public class AddUserTypeRequest
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The name of the user role can contain a maximum of 20 characters.")]
        public string UserRole { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
    }
}
