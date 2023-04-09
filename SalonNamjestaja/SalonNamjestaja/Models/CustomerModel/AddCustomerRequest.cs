using SalonNamjestaja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonNamjestaja.Models.CustomerModel
{
    public class AddCustomerRequest
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The first name of the customer can contain a maximum of 20 characters.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "The last name of the customer can contain a maximum of 20 characters.")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "The phone can contain a maximum of 20 characters.")]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30, ErrorMessage = "The email can contain a maximum of 30 characters.")]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "The username can contain a maximum of 20 characters.")]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "The password must contain a minimum of 8 characters.")]
        [MaxLength(30, ErrorMessage = "The password can contain a maximum of 30 characters.")]
        public string? Password { get; set; }

        [ForeignKey(nameof(Address))]
        [Required]
        public int AddressId { get; set; }

        [ForeignKey(nameof(UserType))]
        [Required]
        public int UserId { get; set; }

  


    }
}
