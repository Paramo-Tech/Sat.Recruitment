using MediatR;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Requests.Queries;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Handlers.QueryHandlers
{
    public class GetProductQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IRepository<User> _userRepository;

        public GetProductQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetById(request.Id);
        }
    }
}
