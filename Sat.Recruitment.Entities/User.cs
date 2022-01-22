using System;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Entities
{
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


        public string UserType { get; set; }
        public decimal Money { get; set; }


    }

}
