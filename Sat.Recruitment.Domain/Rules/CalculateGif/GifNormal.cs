namespace Sat.Recruitment.Domain.Rules.CalculateGif;

public class GifNormal : ICalculateGifStrategy
{
    internal GifNormal()
    {

    }
    public decimal CalculateGif(decimal money)
    {
        if (money > 100)
        {
            return money * 0.12m;
        }
        else if (money > 10)
        {
            return money * 0.8m;
        }
        return 0;
    }
}
