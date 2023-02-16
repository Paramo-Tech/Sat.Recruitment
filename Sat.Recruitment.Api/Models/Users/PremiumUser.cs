namespace Sat.Recruitment.Api.Models.Users
{
    public class PremiumUser : BasicUser
    {
        public override decimal Gift => Money > 100 ? Money * 2 : 0;
    }
}
