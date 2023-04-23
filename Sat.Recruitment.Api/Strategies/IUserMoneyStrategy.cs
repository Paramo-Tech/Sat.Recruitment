namespace Sat.Recruitment.Api.Strategies
{
    public interface IUserMoneyStrategy
    {
        decimal CalculateAdditionalMoney(decimal originalMoney);
    }
}
