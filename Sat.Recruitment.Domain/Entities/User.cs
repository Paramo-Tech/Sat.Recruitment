using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Entities
{
    /// <summary>
    /// User's summary
    /// </summary>
    public class User
    {
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Phone")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "User Type")]
        public string UserType { get; set; } = "Normal";

        [Display(Name = "Money")]
        public decimal Money { get; set; } = 0;
    }
}
