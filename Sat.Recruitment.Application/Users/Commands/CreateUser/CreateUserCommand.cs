using MediatR;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<int>
    {
        public string Name { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Address { get; init; } = default!;
        public string Phone { get; init; } = default!;
        public string UserType { get; init; } = default!;
        public string Money { get; init; } = default!;
    }
}
