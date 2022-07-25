using Sat.Recruitment.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Models.ViewModels
{
    public class UserModel
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [Phone]
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }

        public UserType UserType { get; set; }

        public decimal Money { get; set; }
    }
}