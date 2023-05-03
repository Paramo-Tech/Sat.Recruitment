using FluentValidation;
using MediatR;
using Sat.Recruitment.Api.DbContext;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Requests.Commands;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Handlers.CommandHandlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IRepository<User> _userRepository;
        
        public AddUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = UserFactory.CreateUser(request.userType);
            user.UserType = (UserType)Enum.Parse(typeof(UserType), request.userType);
            user.Name = request.name;
            user.Email = request.email;
            user.Address = request.address;
            user.Phone = request.phone;
            user.Money = Convert.ToDecimal(request.money);
            user.CalculateAllocationToUser();

            await UserExists(user);

            _userRepository.Add(user);

            return user;
        }

        private async Task UserExists(User request)
        {
            if (_userRepository.Exists(request))
                throw new Exception($"already exist user {request.Name} - {request.Email} ");
        }
    }
}
