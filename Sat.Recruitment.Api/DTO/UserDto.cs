using Sat.Recruitment.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTO
{
    public class UserDto
    {
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Text)]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [StringLength(15)]
        public string Phone { get; set; }
        public UserType.Type UserType { get; set; }

        public decimal Money { get; set; }
    }
}
