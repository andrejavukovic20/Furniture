using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.CustomerModel
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage ="Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        [MinLength(8, ErrorMessage = "The password must contain a minimum of 8 characters.")]
        public string Password { get; set; }
    }
}
