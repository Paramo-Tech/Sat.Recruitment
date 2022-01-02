using Sat.Recruitment.Business.Types;
using System;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Helpers;

namespace Sat.Recruitment.Business.Parsers
{
    public class UserParser : IParser<User, string>
    {
        public User Parse(string obj)
        {
            var splittedLine = obj.Split(',');
            var newUser = new User
            {
                Name = splittedLine[0],
                Email = EmailHelper.Normalize(splittedLine[1]),
                Phone = splittedLine[2],
                Address = splittedLine[3],
                UserType = (UserType)Enum.Parse(typeof(UserType), (string)splittedLine[4]),
                Money = decimal.TryParse((string)splittedLine[5], out decimal result) ? result : -1
            };
            return newUser;
        }
        public string Unparse(User obj)
        {
            var user = String.Join(",", obj.Name,
                                      obj.Email,
                                      obj.Phone,
                                      obj.Address,
                                      obj.UserType.ToString(),
                                      obj.Money);
            return user;
        }

    }
}