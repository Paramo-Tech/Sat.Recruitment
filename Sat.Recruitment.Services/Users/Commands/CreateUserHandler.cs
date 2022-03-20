using MediatR;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
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

        }

        private decimal GetPercentage(UserTypeEnum userType, decimal money)
        {
            decimal percentage = 0;
            switch ((int)userType)
            {
                case 1:
                    if (money > 100)
                    {
                        percentage = Convert.ToDecimal(0.12);
                    }
                    else if (money < 100 && money > 10)
                    {
                        percentage = Convert.ToDecimal(0.8);
                    }
                    break;
                case 2:
                    percentage = Convert.ToDecimal(0.20);
                    break;
                default:
                    break;
            }
            return percentage;
        }
    }
}
