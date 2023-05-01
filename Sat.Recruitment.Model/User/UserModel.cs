using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Model.User
{
    public class UserModel
    {

        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number")]
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        [DataType(DataType.Currency)]
        public decimal Money { get; set; }
    }
}
