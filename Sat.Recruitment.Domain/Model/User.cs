using Sat.Recruitment.Domain.Contracts.BonusCalculation;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Model
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
        public string UserType { get; set; }
        public decimal Money { get; set; }

    }
}
