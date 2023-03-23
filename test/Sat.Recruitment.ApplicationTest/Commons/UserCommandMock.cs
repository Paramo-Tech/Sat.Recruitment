using Sat.Recruitment.Application.Commands.CreateUser;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class UserCommandMock
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
    }
}
