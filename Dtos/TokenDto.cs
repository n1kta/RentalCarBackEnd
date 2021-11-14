using System.ComponentModel.DataAnnotations;

namespace RentalCar.Dtos
{
    public class TokenDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
