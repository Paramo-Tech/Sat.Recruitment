using Sat.Recruitment.Infrastructure.Interfaces.Bussiness;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Bussiness
{
    public class UserBussiness : IUserBussiness
    {
        public ICollection<User> Users { get; set; }

        public UserBussiness(ICollection<User> users)
        {
            Users = users;
        }
        public void CreateUser(User newUser)
        {
            ValidateUser(newUser);
            Users.Add(newUser);
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
