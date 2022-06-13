using Users.Application.Commands.Create;
using Users.Domain;

namespace Users.UnitTest.Users.Application.Commands.Create
{
    public static class DataProvider
    {
        public static CreateUserCommand ValidCommand() => new()
        {
            Name = "Mike",
            Email = new("mike@gmail.com"),
            Address = "Av. Juan G",
            Phone = new("+349 1122354215"),
            UserType = UserType.Normal,
            Money = 124
        };

        public static User BaseUser() => new(
            "Mike",
            new("mike@gmail.com"),
            "Av. Juan G",
            new("+349 1122354215"),
            UserType.Normal,
            124
        );

        public static IEnumerable<object[]> InvalidCommands()
        {
            yield return new object[] { EmptyCommand() };
            yield return new object[] { EmptyNameCommand() };
            yield return new object[] { EmptyAddressCommand() };
        }

        private static CreateUserCommand EmptyCommand() => new();

        private static CreateUserCommand EmptyNameCommand()
        {
            var command = ValidCommand();
            command.Name = String.Empty;

            return command;
        }

        private static CreateUserCommand EmptyAddressCommand()
        {
            var command = ValidCommand();
            command.Address = String.Empty;

            return command;
        }
    }
}
