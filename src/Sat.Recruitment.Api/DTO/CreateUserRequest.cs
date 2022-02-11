using Microsoft.AspNetCore.Mvc;

namespace Sat.Recruitment.Api.DTO
{
    public class CreateUserRequest
    {
        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "email")]
        public string Email { get; set; }

        [BindProperty(Name = "address")]
        public string Address { get; set; }

        [BindProperty(Name = "phone")]
        public string Phone { get; set; }

        [BindProperty(Name = "userType")]
        public string UserType { get; set; }

        [BindProperty(Name = "money")]
        public string Money { get; set; }
    }
}
