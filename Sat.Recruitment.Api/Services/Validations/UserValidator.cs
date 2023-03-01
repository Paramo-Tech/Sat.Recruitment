using FluentValidation;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Services.Validations
{
    public class UserValidator: AbstractValidator<User>
    {
        private readonly IList<User> users;

        public UserValidator(IList<User> users)
        {
            this.users = users;
        }

        public bool ExistByPhoneAndEmail(User user)
        {
            return users.Any(u => u.Phone == user.Phone || u.Email == user.Email);
        }

        public bool ExistByNameAndAddress(User user)
        {
            return users.Any(u => u.Name == user.Name && u.Address == user.Address);
        }

        public bool IsDuplicated(User user)
        {
            return ExistByPhoneAndEmail(user) || ExistByNameAndAddress(user);
        }
    }
}
