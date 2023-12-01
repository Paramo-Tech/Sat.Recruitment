using Sat.Recruitment.DTOs.ValidationAttributeHelper;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.DTOs
{
    public class UserDTO
    {

        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "The phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = "";
        public string UserType { get; set; } = "";

        [ParseableToNumber(ErrorMessage = "Invalid number format")]
        public string Money { get; set; } = "";
    }
}