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
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery,List<User>>
    {
        private readonly IUsersRepository _repository;
        public GetAllUsersHandler(IUsersRepository repository) => _repository = repository;

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
