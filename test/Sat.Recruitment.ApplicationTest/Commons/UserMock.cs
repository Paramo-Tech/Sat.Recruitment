using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class UserMock
    {
        public static User DefaultUser => new("angel cruz", "angel@gmail.com", "av poeta lugones", "+543884616189", "Normal", 100, 1);
        public static User NormalUser => new("angel cruz", "angel@gmail.com", "av poeta lugones", "+543884616189", "Normal", 100, 2);
        public static User PremiumUser => new("angel cruz", "angel@gmail.com", "av poeta lugones", "+543884616189", "Premium", 100, 3);
        public static User SuperUser => new("angel cruz", "angel@gmail.com", "av poeta lugones", "+543884616189", "SuperUser", 100, 4);
    }
}
