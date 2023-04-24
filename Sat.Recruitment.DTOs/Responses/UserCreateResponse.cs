using Sat.Recruitment.DTOs.Models;

namespace Sat.Recruitment.DTOs.Responses
{
    public class UserCreateResponse
    {
        public bool Success { get; set; }
        public User User { get; set; }
    }
}