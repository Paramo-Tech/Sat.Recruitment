using MediatR;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Domain.Enum;

namespace Sat.Recruitment.Application.Command
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public UserType UserType { get; set; }

        public decimal Money { get; set; }
    }
}
