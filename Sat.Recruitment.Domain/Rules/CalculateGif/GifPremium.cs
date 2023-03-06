namespace Sat.Recruitment.Domain.Rules.CalculateGif;

public class GifPremium : ICalculateGifStrategy
{
    internal GifPremium()
    {

    }
    public decimal CalculateGif(decimal money)
    {
        if (money > 100)
        {
            return money * 2;
        }        
        return 0;
    }
}
