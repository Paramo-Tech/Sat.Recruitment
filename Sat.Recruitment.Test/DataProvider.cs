using Sat.Recruitment.Api.Controllers;

namespace Sat.Recruitment.Test
{
    public static class DataProvider
    {
        public static CreateUserDto ValidCreateUserRequest() => new ()
        {
            Name = "Mike",
            Email = "mike@gmail.com",
            Address = "Av. Juan G",
            Phone = "+349 1122354215",
            UserType = "Normal",
            Money = 124
        };
    }
}
