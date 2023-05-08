using Sat.Recruitment.Domain.Model;

namespace Sat.Recruitment.Application.Command
{
    public static class CreateUserCommandExtensions
    {
        public static User ToUser(this CreateUserCommand createUserCommand)
        {
            return new User(createUserCommand.Name,
                            createUserCommand.Email,
                            createUserCommand.Phone,
                            createUserCommand.Address,
                            createUserCommand.UserType,
                            createUserCommand.Money);
        }
    }
}
