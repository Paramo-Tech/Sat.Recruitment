using Users.Domain;

namespace Users.Application.Commands.Create
{
    public static class CreateUserCommandMapper
    {
        public static User Execute(CreateUserCommand createUserCommand) => new()
        {
            Name = createUserCommand.Name,
            Email = createUserCommand.Email,
            Address = createUserCommand.Address,
            Phone = createUserCommand.Phone,
            UserType = createUserCommand.UserType,
            Money = createUserCommand.Money
        };
    }
}
