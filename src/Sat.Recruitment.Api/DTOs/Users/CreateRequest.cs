using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Core.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTOs.Users
{
    public class CreateRequest
    {
        [BindProperty(Name = "name")]
        [Required(ErrorMessage = "The name is required")]
        [SwaggerParameter(Description = "Name of the User", Required = true)]
        public string Name { get; set; }

        [BindProperty(Name = "email")]
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress]
        [SwaggerParameter(Description = "Email of the User", Required = true)]
        public string Email { get; set; }

        [BindProperty(Name = "address")]
        [Required(ErrorMessage = "The address is required")]
        [SwaggerParameter(Description = "Address of the User", Required = true)]
        public string Address { get; set; }

        [BindProperty(Name = "phone")]
        [Required(ErrorMessage = "The phone is required")]
        [Phone]
        [SwaggerParameter(Description = "Phone of the User", Required = true)]
        public string Phone { get; set; }

        [BindProperty(Name = "userType")]
        [SwaggerParameter(Description = "UserType of the User", Required = false)]
        public UserType? UserType { get; set; }

        [BindProperty(Name = "money")]
        [SwaggerParameter(Description = "Money of the User", Required = false)]
        public decimal Money { get; set; }
    }
}
