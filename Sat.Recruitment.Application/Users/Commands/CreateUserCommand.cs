using MediatR;
using Sat.Recruitment.Application.Common.Mappings;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<Result>, IMapTo<User>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public decimal Money { get; set; }
    }
}
