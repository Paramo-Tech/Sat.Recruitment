using Sat.Recruitment.Infrastructure.Interfaces.Bussiness;
using Sat.Recruitment.Infrastructure.Interfaces.DataAccess;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Bussiness
{
    public class UserBussiness : IUserBussiness
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserBussiness(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }
        public void CreateUser(User newUser)
        {
            ValidateUser(newUser);
            NormalizeEmail(newUser);
            CheckUserDuplication(newUser);
            _userDataAccess.CreateEntity(newUser);
        }

        private void CheckUserDuplication(User user)
        {
            var userFind = _userDataAccess.GetSingleBy((usr) => usr.Email == user.Email || (user.Name == usr.Name && user.Address == usr.Address));
            if(userFind!=null)
            throw new Exception("The user is duplicated");
        }


        private void NormalizeEmail(User user)
        {
            var emailSplit = user.Email.Split('@', StringSplitOptions.RemoveEmptyEntries);

            var atIndex = emailSplit[0].IndexOf('+', StringComparison.Ordinal);

            emailSplit[0] = atIndex < 0 ? emailSplit[0] : emailSplit[0].Remove(atIndex);

            user.Email = $"{emailSplit[0].Replace(".", "")}@{emailSplit[1]}";
        }

        private void ValidateUser(User user)
        {
            StringBuilder errors = new StringBuilder();
            if (user.Name == null)
                //Validate if Name is null
                errors.Append("The name is required");
            if (user.Email == null)
                //Validate if Email is null
                errors.Append(" The email is required");
            if (user.Address == null)
                //Validate if Address is null
                errors.Append(" The address is required");
            if (user.Phone == null)
                //Validate if Phone is null
                errors.Append(" The phone is required");

            if (errors.Length > 0)
                throw new Exception(errors.ToString());
        }
    }
}
