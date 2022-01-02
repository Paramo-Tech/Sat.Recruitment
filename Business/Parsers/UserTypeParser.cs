using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Parsers
{
    public class UserTypeParser : IParser<UserType, string>
    {
        public UserType Parse(string obj)
        {
          return (UserType)Enum.Parse(typeof(UserType),obj);
        }

        public string Unparse(UserType obj)
        {
            return obj.ToString();
        }
    }
}
