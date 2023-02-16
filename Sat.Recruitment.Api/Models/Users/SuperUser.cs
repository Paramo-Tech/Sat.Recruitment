namespace Sat.Recruitment.Api.Models.Users
{
    public class SuperUser : BasicUser
    {
        public override decimal Gift => Money > 100 ? Money * 0.20m : 0;
    }
}
