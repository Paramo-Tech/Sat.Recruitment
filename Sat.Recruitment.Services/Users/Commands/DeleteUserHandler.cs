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

namespace Sat.Recruitment.Services.Users.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly IUsersRepository _repository;
        public DeleteUserHandler(IUsersRepository repository) => _repository = repository;

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Remove(request.Id, cancellationToken);

            return user;
        }
    }
}
