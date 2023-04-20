using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        public decimal Money { get; set; }
    }
}
