using MediatR;
using Shared.Domain;
using Users.Domain;

namespace Users.Application.Commands.Create
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }

        public Email Email { get; set; }

        public string Address { get; set; }

        public Phone Phone { get; set; }

        public UserType UserType { get; set; }

        public decimal Money { get; set; }
    }
}
