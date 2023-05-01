using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

public class UserGifService : IUserGifService
{
    private readonly IEnumerable<IUserGifStrategy> _strategies;

    public UserGifService(IEnumerable<IUserGifStrategy> strategies)
    {
        _strategies = strategies;
    }

    public decimal Calculate(User user)
    {
        var strategy = GetStrategy(user.UserType);

        var result = strategy.Calculate(user);

        return result;
    }

    private IUserGifStrategy GetStrategy(string userType) 
        => _strategies.FirstOrDefault(s => s.UserType == userType) 
           ?? throw new InvalidOperationException($"Strategy for user {userType} not found");
}