using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Common.ParseDecimal;
using Sat.Recruitment.Common.FormatEmail;
using Sat.Recruitment.Persistence.Interfaces;
using System.Linq;
using Sat.Recruitment.Domain.Exceptions;

namespace Sat.Recruitment.Business.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IUserFileRepository userFileRepository;
        public CreateUserCommandHandler(IUserFileRepository userFileRepository)
        {
            this.userFileRepository = userFileRepository;
        }

        public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = GetModelByCommand(request);
            try
            {
                await ValidateIsUserExist(newUser);
                string userLine = ConvertUserLine(newUser);
                userFileRepository.SaveUserInFileAsync(userLine);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new CreateUserResult(false, message);
            }
            return new CreateUserResult(true, "User Created");
        }


        private decimal SetMoneyByUserType(UserType userType, decimal money)
        {

            switch (userType)
            {
                case UserType.Normal:
                    return ParseDecimalByUser.MoneyTypeNormal(money);

                case UserType.SuperUser:
                    return ParseDecimalByUser.MoneyTypeSuperUser(money);

                case UserType.Premium:
                    return ParseDecimalByUser.MoneyTypePremium(money);
            }
            return money;
        }

        private string ConvertUserLine(UserE user)
        {
            return  $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money} {Environment.NewLine}";
        }

        private UserE GetModelByCommand(CreateUserCommand request)
        {
            var newUser = new UserE
            {
                Name = request.name,
                Address = request.address,
                Phone = request.phone,
                Email = UserFormatEmail.NormalizeEmail(request.email),
                UserType = UserTypeString.GetByName(request.userType),

            };

            decimal money = ParseString.TryParseToDecimal(request.money);
            newUser.Money = SetMoneyByUserType(newUser.UserType, money);
            return newUser;
        }

        private async Task ValidateIsUserExist(UserE request)
        {
            if(IsDuplicatedUser(request))
                throw new CreateUserException<UserE>($"The User {request.Email} already Exist");
        }

        private bool IsDuplicatedUser(UserE user)
        {
            var allUsers = userFileRepository.GetAllUsersAsync().Result;

            if (allUsers.Any(u => u.Phone == user.Phone || u.Email == user.Email))
                return true;

            if (allUsers.Any(u => u.Name == user.Name && u.Address == user.Address))
                return true;

            return false;
        }
    }
}
