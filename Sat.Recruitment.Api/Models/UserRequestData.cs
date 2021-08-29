using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models
{
    public class UserRequestData
    {
        [Required(ErrorMessage= "The name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "The email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "The address is required")]
        public string address { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        public string phone { get; set; }
        public UserType userType { get; set; }
        public string money { get; set; }
    }
}
