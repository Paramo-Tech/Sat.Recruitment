namespace Sat.Recruitment.Domain.Rules.CalculateGif;

public class GifSuperUser : ICalculateGifStrategy
{
    internal GifSuperUser()
    {

    }
    public decimal CalculateGif(decimal money)
    {
        if (money > 100)
        {
            return money * 0.2m;
        }
        return 0;
    }
}
