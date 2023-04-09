using System.ComponentModel.DataAnnotations;

namespace SalonNamjestaja.Models.CustomerModel
{
    public class AdminRequestDto
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; } = null!;
    }
}
