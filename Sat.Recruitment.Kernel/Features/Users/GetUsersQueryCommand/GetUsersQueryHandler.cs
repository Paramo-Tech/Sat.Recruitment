using MediatR;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Kernel.Features.Users.GetUsersQueryCommand
{
    /// <summary>
    /// Handler for getting users
    /// </summary>
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, List<IUser>>
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Handler to get all users
        /// </summary>
        /// <param name="request">Request parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of users</returns>
        public async Task<List<IUser>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAsync();
            return users.ToList();
        }
    }
}
