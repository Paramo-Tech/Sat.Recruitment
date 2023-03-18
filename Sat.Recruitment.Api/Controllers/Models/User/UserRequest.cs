using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Controllers
{
    public class UserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Phone { get; set; }
        [Required]
        public string UserType { get; set; }
        public string Money { get; set; }
    }
}
