using Application.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<UserDto> 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserTypes UserType { get; set; }
        public decimal Money { get; set; }
    }
}
