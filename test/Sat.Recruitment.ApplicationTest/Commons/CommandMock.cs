using Sat.Recruitment.Application.Commands.CreateUser;
using Sat.Recruitment.Application.Queries.GetUser;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class CommandMock
    {
        public static CreateUserCommand SomeUser=> new CreateUserCommand() 
        { 
            Address = "av poeta lugones",
            Email = "angel@gmail.com",
            Money = 100,
            Name = "Angel",
            Phone = "+543884618189",
            UserType = "Normal"
        };

        public static GetUserQuery SomeUserId => new GetUserQuery() { Id = 1 };
    }
}
