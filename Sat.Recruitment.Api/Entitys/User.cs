using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Entitys
{
    public class User
    {

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "The Address is required")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "The Phone is required")]
        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; }
    }
}
