using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.ExtensionMethods;

namespace Sat.Recruitment.Services.Implementations.UsersType
{
     class Premium : AbstractHandler
    {
        public override User Handle(User user)
        {
            if (user.UserType == UserTypes.Premium.GetDescription())
            {
                if (user.Money > 100)
                {
                    var gif = user.Money * 2;
                    user.Money = user.Money + gif;
                }
                return user;
            }
            else
            {
                return base.Handle(user);
            }
        }
    }
}
