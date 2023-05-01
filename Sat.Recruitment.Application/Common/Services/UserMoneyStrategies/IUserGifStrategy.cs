namespace Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

public interface IUserGifStrategy : IUserGifService
{
    public string UserType { get; }
}