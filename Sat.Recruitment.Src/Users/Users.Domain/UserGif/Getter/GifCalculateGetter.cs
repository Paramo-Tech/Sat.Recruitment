using Users.Domain.UserGif.Calculators;

namespace Users.Domain.UserGif.Getter
{
    public class GifCalculateGetter : IGifCalculateGetter
    {
        private readonly Dictionary<UserType, ICalculateUserGif> gitCalculators = new()
        {
            { UserType.Normal, new CalculateNormalUserGif() },
            { UserType.Premium, new CalculatePremiunUserGif() },
            { UserType.SuperUser, new CalculateSuperUserGif() }
        };

        public ICalculateUserGif GetCalculator(UserType userType)
        {
            return gitCalculators[userType];
        }
    }
}
