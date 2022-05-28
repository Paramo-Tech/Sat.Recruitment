using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Domain
{
    public class User : IUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}