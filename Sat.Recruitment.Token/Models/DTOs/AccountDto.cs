using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Token.Models.DTOs
{
    public class AccountDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
