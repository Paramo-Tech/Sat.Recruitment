using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTO
{
    public class userCreateDTO
    {
        [Required(ErrorMessage = "The name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "The email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "The address is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "The phone is required")]
        public string phone { get; set; }
        public string userType { get; set; }
        public string money { get; set; }
    }
}
