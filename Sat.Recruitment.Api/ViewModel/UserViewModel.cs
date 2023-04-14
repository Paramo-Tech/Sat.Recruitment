using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailValidation]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
