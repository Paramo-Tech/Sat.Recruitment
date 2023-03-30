using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// User's summary
    /// </summary>
    public class User
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Display(Name = "Money")]
        public decimal Money { get; set; }
    }
}
