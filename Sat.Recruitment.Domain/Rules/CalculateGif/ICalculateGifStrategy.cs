namespace Sat.Recruitment.Domain.Rules.CalculateGif;

public interface ICalculateGifStrategy
{
    decimal CalculateGif(decimal money);

    public delegate ICalculateGifStrategy ServiceResolve(UserType userType);
}
