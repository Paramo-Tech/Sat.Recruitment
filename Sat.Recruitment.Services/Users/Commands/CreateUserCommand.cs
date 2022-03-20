using MediatR;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Users.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserTypeEnum UserType { get; set; }
        public decimal Money { get; set; }
        public CreateUserCommand(UserDto user)
        {
            Name = user.Name;
            Email = user.Email;
            Address = user.Address;
            Phone = user.Phone;
            UserType = (UserTypeEnum) user.UserType;
            Money = Convert.ToDecimal(user.Money);
        }
    }
}