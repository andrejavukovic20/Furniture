using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.CustomerModel
{
    public class RegisterRequestDto
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

        public string Phone { get; set; } 

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The password must contain a minimum of 8 characters.")]
        public string? Password { get; set; }

        public int AddressId { get; set; }


    }
}
