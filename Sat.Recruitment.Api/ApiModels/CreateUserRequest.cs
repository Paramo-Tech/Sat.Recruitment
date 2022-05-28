using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Api.ApiModels
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}