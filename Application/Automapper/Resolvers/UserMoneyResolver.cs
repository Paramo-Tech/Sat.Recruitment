using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Factories;
using Domain.Strategies;

namespace Application.Automapper.Resolvers
{
    public class UserMoneyResolver : IValueResolver<CreateUserCommand, User, decimal>
    {
        private readonly IUserMoneyStrategyFactory _userMoneyStrategyFactory;

        public UserMoneyResolver(IUserMoneyStrategyFactory userMoneyStrategyFactory)
        {
            _userMoneyStrategyFactory = userMoneyStrategyFactory;
        }

        public decimal Resolve(CreateUserCommand source, User destination, decimal destMember, ResolutionContext context)
        {
            IUserMoneyStrategy userMoneyStrategy = _userMoneyStrategyFactory.BuildUserMoneyStrategy(source.UserType);
            return userMoneyStrategy.CalculateUserMoneyAmount(source.Money);
        }
    }
}
