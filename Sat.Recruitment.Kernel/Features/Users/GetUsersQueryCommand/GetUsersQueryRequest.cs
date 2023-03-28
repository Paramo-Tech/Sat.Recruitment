using MediatR;
using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Kernel.Features.Users.GetUsersQueryCommand
{
    /// <summary>
    /// Request for getting users
    /// </summary>
    public class GetUsersQueryRequest : IRequest<List<IUser>>
    {
    }
}
