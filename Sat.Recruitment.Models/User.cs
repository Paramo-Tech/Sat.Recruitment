using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Models
{
    public class User
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\+?\d{1,3}[-.\s]?\(?\d{1,3}\)?[-.\s]?\d{3,4}[-.\s]?\d{4}$", ErrorMessage = "Enter a valid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "UserType is required")]
        public string UserType { get; set; }

        [Required(ErrorMessage = "Money is required")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Enter a valid numeric value.")]
        public decimal Money { get; set; }
    }
}
