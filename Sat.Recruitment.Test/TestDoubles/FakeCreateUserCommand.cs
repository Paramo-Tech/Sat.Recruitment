using Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.TestDoubles
{
    public static class FakeCreateUserCommand
    {
        private static CreateUserCommand _cmd = new CreateUserCommand
        {
            Name = "Test Testing",
            Email = "unit@test.com",
            UserType = Domain.Enums.UserTypes.Normal,
            Money = 100,
            Address = "Calle Falsa 123",
            Phone = "4123456"
        };

        public static CreateUserCommand WithValidData => new CreateUserCommand
        {
            Name = _cmd.Name,
            Email = _cmd.Email,
            UserType = _cmd.UserType,
            Money = _cmd.Money,
            Address = _cmd.Address,
            Phone = _cmd.Phone
        };
        public static CreateUserCommand WithEmptyName => new CreateUserCommand
        {
            Name = string.Empty,
            Email = _cmd.Email,
            UserType = _cmd.UserType,
            Money = _cmd.Money,
            Address = _cmd.Address,
            Phone = _cmd.Phone
        };
        public static CreateUserCommand WithEmptyEmail => new CreateUserCommand
        {
            Name = _cmd.Name,
            Email = String.Empty,
            UserType = _cmd.UserType,
            Money = _cmd.Money,
            Address = _cmd.Address,
            Phone = _cmd.Phone
        };
        public static CreateUserCommand WithInvalidEmail => new CreateUserCommand
        {
            Name = _cmd.Name,
            Email = "InvalidEmail",
            UserType = _cmd.UserType,
            Money = _cmd.Money,
            Address = _cmd.Address,
            Phone = _cmd.Phone
        };
        public static CreateUserCommand WithEmptyAddress => new CreateUserCommand
        {
            Name = _cmd.Name,
            Email = _cmd.Email,
            UserType = _cmd.UserType,
            Money = _cmd.Money,
            Address =String.Empty,
            Phone = _cmd.Phone
        };
        public static CreateUserCommand WithEmptyPhone => new CreateUserCommand
        {
            Name = _cmd.Name,
            Email = _cmd.Email,
            UserType = _cmd.UserType,
            Money = _cmd.Money,
            Address = _cmd.Address,
            Phone = String.Empty
        };
        public static CreateUserCommand WithNegativeMoney=> new CreateUserCommand
        {
            Name = _cmd.Name,
            Email = _cmd.Email,
            UserType = _cmd.UserType,
            Money = -1,
            Address = _cmd.Address,
            Phone = _cmd.Phone
        };
    }
}
