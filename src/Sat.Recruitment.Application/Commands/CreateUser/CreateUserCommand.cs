using System;
using MediatR;

namespace Sat.Recruitment.Application.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<CreateUserCommandResponse>
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}

