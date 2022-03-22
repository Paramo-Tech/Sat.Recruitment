using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repository.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Users.Queries
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUsersRepository _repository;
        public GetUserHandler(IUsersRepository repository) => _repository = repository;

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Id, cancellationToken);
        }
    }
}
