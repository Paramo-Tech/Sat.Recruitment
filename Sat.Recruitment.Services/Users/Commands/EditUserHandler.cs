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
    public class EditUserHandler : IRequestHandler<EditUserCommand, User>
    {
        private readonly IUsersRepository _repository;
        public EditUserHandler(IUsersRepository repository) => _repository = repository;
        public async Task<User> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(request.User.Id, cancellationToken);

            if (user == null)
                return null;

            byte[] cryptedPassword = null;
            if (!String.IsNullOrEmpty(request.User.Password))
                cryptedPassword = Encoding.ASCII.GetBytes(request.User.Password);

            if (!String.IsNullOrEmpty(request.User.Address) && request.User.Address != user.Address)
                user.Address = request.User.Address;
            if ((UserTypeEnum)request.User.UserType != user.UserType)
                user.UserType = (UserTypeEnum)request.User.UserType;
            if (cryptedPassword != null && cryptedPassword != user.Password)
                user.Password = cryptedPassword;

            var updatedUser = await _repository.UpdateAsync(user, cancellationToken);

            return updatedUser;
        }
    }
}
