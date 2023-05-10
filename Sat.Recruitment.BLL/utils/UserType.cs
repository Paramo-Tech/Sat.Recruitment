using System;

namespace Sat.Recruitment.BLL.utils
{
    public static class UserType
    {
        public const string NORMAL = "NORMAL";
        public const string SUPERUSER = "SUPERUSER";
        public const string PREMIUM = "PREMIUM";

        public const double NORMAL_PERC = 0.12;
        public const double SUPERUSER_PERC = 0.20;
        public const decimal PREMIUM_PERC = 2;
        public const double NORMAL_PERC_LESS_100 = 0.8;
    }
}
