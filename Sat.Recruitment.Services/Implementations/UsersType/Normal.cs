using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.ExtensionMethods;

namespace Sat.Recruitment.Services.Implementations.UsersType
{
    class Normal : AbstractHandler
    {
        public override User Handle(User user)
        {
            if (user.UserType == UserTypes.Normal.GetDescription())
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = user.Money * percentage;
                    user.Money = user.Money + gif;
                }
                if (user.Money < 100)
                {
                    if (user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        user.Money = user.Money + gif;
                    }
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
