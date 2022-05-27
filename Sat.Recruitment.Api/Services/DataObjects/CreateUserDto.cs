using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Api.Services.DataObjects
{
    
    public sealed class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}