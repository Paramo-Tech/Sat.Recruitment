using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.DTOs.Responses;
using Sat.Recruitment.Services.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Services.Imp
{
    public class UsersService : IUsersService
    {
        private readonly IGetUsersCommand getUsersCommand;
        private readonly INormalizeEmailCommand normalizeEmailCommand;
        private readonly ICreateUserCommand createUserCommand;
        private readonly IUserExistsCommand userExistsCommand;
        private readonly ICalculateMoneyIncreaseCommand calculateMoneyIncreaseCommand;

        public UsersService(
            IGetUsersCommand getUsersCommand,
            INormalizeEmailCommand normalizeEmailCommand,
            ICreateUserCommand createUserCommand,
            IUserExistsCommand userExistsCommand,
            ICalculateMoneyIncreaseCommand calculateMoneyIncreaseCommand)
        {
            this.getUsersCommand = getUsersCommand;
            this.normalizeEmailCommand = normalizeEmailCommand;
            this.createUserCommand = createUserCommand;
            this.userExistsCommand = userExistsCommand;
            this.calculateMoneyIncreaseCommand = calculateMoneyIncreaseCommand;
        }
        public async Task<UserCreateResponse> Create(UserCreateRequest request)
        {
            var users = await this.getUsersCommand.Execute();

            // Normalize the Emails of the file and request before to be compared
            users.ForEach(x => x.Email = this.normalizeEmailCommand.Execute(x.Email));
            request.Email = this.normalizeEmailCommand.Execute(request.Email);

            var userExists = this.userExistsCommand.Execute(users, request);
            if (userExists)
            {
                return new UserCreateResponse { Success = false };
            }
            request.Money = this.calculateMoneyIncreaseCommand.Execute(request.UserType, request.Money);
            return await this.createUserCommand.Execute(request);
        }
    }
}