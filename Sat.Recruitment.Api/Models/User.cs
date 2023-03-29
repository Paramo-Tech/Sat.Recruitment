using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// User's summary
    /// </summary>
    public class User
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }

        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Display(Name = "Money")]
        public decimal Money { get; set; }
    }
}
