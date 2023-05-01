using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

public interface IUserGifService
{
    public decimal Calculate(User user);
}