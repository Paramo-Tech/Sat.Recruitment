using MediatR;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Services.Strategy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly UsersContext _context;
        public CreateUserHandler(UsersContext context) => _context = context;
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Money = CreateUserHelper.HandleMoneyStrategy(request.UserType, request.Money);
            request.Email = CreateUserHelper.NormalizeMail(request.Email);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Money = request.Money,
                Address = request.Address,
                IsActive = true,
                Phone = request.Phone,
                UserType = request.UserType,
                Password = Encoding.ASCII.GetBytes(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
