using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Domain.Contracts
{
    public interface IUserModel
    {
        string Name { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        UserType UserType { get; set; }
        decimal Money { get; set; }
    }
}