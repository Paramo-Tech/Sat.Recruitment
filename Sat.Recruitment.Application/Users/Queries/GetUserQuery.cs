using MediatR;

namespace Sat.Recruitment.Application.Users.Queries;

public record struct GetUserQuery : IRequest<IList<GetUserDto>>
{
}
