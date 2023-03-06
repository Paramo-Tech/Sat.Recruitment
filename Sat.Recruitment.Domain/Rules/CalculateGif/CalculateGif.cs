using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Rules.CalculateGif
{
    public static class CalculateGif
    {
        public static ICalculateGifStrategy CreateCalculateGif(UserType userType)
        {
            return userType switch
            {
                UserType.Normal => new GifNormal(),
                UserType.Premium => new GifPremium(),
                UserType.SuperUser => new GifSuperUser(),
                _ => new GifNormal(),
            };
        }
    }
}
