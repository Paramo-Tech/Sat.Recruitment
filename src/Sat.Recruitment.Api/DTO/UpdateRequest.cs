using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTO
{
    public class UpdateRequest
    {
        [BindProperty(Name = "name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [BindProperty(Name = "email")]
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty(Name = "address")]
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [BindProperty(Name = "phone")]
        [Required(ErrorMessage = "The phone is required")]
        [Phone]
        public string Phone { get; set; }

        [BindProperty(Name = "userType")]
        public UserType? UserType { get; set; }

        [BindProperty(Name = "money")]
        public decimal Money { get; set; }
    }
}
