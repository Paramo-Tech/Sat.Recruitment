using Users.Domain.UserGif.Calculators;

namespace Users.Domain.UserGif.Getter
{
    public interface IGifCalculateGetter
    {
        ICalculateUserGif GetCalculator(UserType userType);
    }
}
