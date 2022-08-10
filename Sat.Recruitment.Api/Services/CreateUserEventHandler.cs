using MediatR;
using Sat.Common;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Commands;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class CreateUserEventHandler : IRequestHandler<CreateUserCommand, int>
    {
        public CreateUserEventHandler()
        {
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var money = request.money.ToDecimal();
            var newUser = new User
            {
                Name = request.name,
                Email = Utils.NormalizeEmail(request.email),
                Address = request.address,
                Phone = request.phone,
                UserType = UserTypeString.GetByName(request.userType),
                
            };

            newUser.Money = SetMoneyByUserType(newUser.UserType, money);

            var userArray = new string[] { newUser.Name, newUser.Email, newUser.Phone, newUser.Address, newUser.UserType.ToString(), newUser.Money.ToString("#.00").Replace(",",".") };
            var userLine = new string[] { string.Join(",", userArray) };
            FileRepository.AppendLines(userLine);

            return await Task.FromResult(0);
        }

        private decimal SetMoneyByUserType(UserType userType, decimal money)
        {
            var result = 0M;
            switch (userType)
            {
                case UserType.Normal:
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = money * percentage;
                        result = money + gif;
                    }
                    if (money < 100)
                    {
                        if (money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = money * percentage;
                            result = money + gif;
                        }
                    }
                    break;
                case UserType.SuperUser:
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = money * percentage;
                        result = money + gif;
                    }
                    break;
                case UserType.Premium:
                    if (money > 100)
                    {
                        var gif = money * 2;
                        result = money + gif;
                    }
                    break;
            }
            return result;
        }
    }
}
