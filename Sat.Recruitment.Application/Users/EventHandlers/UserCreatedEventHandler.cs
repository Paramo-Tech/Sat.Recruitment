using MediatR;
using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Users.EventHandlers;

public class UserCreatedEventHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserGifService _userGifService;

    public UserCreatedEventHandler(IApplicationDbContext dbContext,
        IUserGifService userGifService)
    {
        _dbContext = dbContext;
        _userGifService = userGifService;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Address = request.Address,
            Phone = request.Phone,
            UserType = request.UserType,
            Money = decimal.Parse(request.Money)
        };

        user.Money = _userGifService.Calculate(user);
        user.Email = user.Email.NormalizeEmail();

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}