using MediatR;
using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Kernel.Features.Users.GetUserQueryByIdCommand
{
    /// <summary>
    /// Handler for user by id query command
    /// </summary>
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, IUser>
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService">User service to get data</param>
        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Handler to get user by id
        /// </summary>
        /// <param name="request">Request parameter</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IUser</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IUser> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            request = request?? throw new ArgumentNullException(nameof(request));
            request.Id = request.Id?? throw new ArgumentNullException(nameof(request.Id));

            var user = await _userService.GetAsyncById(request.Id);

            if (user == null)
            {
                throw new NotFoundException($"User id '{request.Id.ToString()}' not found.");
            }

            return user;
        }
    }
}
