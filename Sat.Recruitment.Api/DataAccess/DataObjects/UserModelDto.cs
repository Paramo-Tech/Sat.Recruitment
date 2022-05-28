using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Domain.Contracts;

namespace Sat.Recruitment.Api.DataAccess.DataObjects
{
 
    public sealed class UserModelDto: IUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}