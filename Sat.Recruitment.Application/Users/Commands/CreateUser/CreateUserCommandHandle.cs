using MediatR;
using Sat.Recruitment.Application.Base.Interfaces;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Rules.CalculateGif;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandle(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var calculateGifStrategy = CalculateGif.CreateCalculateGif(request.UserType);

        var entity = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Address = request.Address,
            Phone = request.Phone,
            UserType = request.UserType,
            Money = request.Money + calculateGifStrategy.CalculateGif(request.Money),
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
