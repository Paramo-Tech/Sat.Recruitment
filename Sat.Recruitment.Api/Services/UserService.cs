using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Models;
using System;
using System.Linq;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : BaseEntityService<User>, IUserService
    {
        protected override string FileName => "Users";

        public override bool Create(User entity)
        {
            var users = GetAll();
            return !users.Any(u => u.Email == entity.Email || u.Phone == entity.Phone || (u.Name == entity.Address && u.Address == entity.Address));
        }

        protected override User FormatEntity(string[] fields)
        {
            return new User
            {
                Name = fields[0],
                Email = fields[1],
                Phone = fields[2],
                Address = fields[3],
                UserType = (UserType)Enum.Parse(typeof(UserType), fields[4]),
                Money = decimal.Parse(fields[5]),
            };

        }
    }
}
