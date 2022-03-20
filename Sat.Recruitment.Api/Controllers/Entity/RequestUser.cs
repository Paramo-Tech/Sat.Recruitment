using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers.Entity
{
    public class RequestUser
    {
        [Display(Name = "Name")] // <-- Here
        [Required(ErrorMessage = "{0} is required.")]
        public string Name { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Phone { get; set; }

        [Display(Name = "UserType")]
        [Required(ErrorMessage = "{0} is required.")]
        public string UserType { get; set; }

        [Display(Name = "Money")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Money { get; set; }
    }

}
