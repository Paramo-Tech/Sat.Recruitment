using Sat.Recruitment.DTOs.Enums;

namespace Sat.Recruitment.DTOs.Requests
{
    public class UserCreateRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public double Money { get; set; }
    }
}