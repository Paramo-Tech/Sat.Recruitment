
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Application.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "The userType is required")]
        public string UserType { get; set; }
        [Required(ErrorMessage = "The money is required")]
        public string Money { get; set; }
    }
}
