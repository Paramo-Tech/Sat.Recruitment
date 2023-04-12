using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "The field Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is a required field.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        public string Phone { get; set; }

        public string UserType { get; set; }

        [Required(ErrorMessage = "Money is a required field.")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Money field must be decimal")]
        public decimal Money { get; set; }


        
    }
}
