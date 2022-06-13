using Users.Domain;

namespace Users.Application.Commands.Create
{
    public static class CreateUserCommandMapper
    {
        public static User Execute(CreateUserCommand createUserCommand) => new(
            createUserCommand.Name,
            createUserCommand.Email,
            createUserCommand.Address,
            createUserCommand.Phone,
            createUserCommand.UserType,
            createUserCommand.Money
        );
    }
}
