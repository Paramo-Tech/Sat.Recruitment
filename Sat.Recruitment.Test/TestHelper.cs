using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test
{
    public static class TestHelper
    {

        public static UsersContext GenerateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<UsersContext>()
               .UseInMemoryDatabase(databaseName: "MockDB")
               .Options;

            return new UsersContext(options);
        }
        public static void InitializeTestUsersCreationFormList(this List<UserCreationForm> users)
        {
            users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake@mail.com",
                Money = "1234",
                Name = "John Doe",
                Password = "Pwd1",
                Phone = "12345688",
                UserType = 2
            });
            users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake2@mail.com",
                Money = "1234",
                Name = "John Johnson",
                Password = "Pwd1",
                Phone = "123456884",
                UserType = 2
            });
            users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake2@mail.com",
                Money = "1234",
                Name = "John Jackson",
                Password = "Pwd1",
                Phone = "1234568845",
                UserType = 2
            });
            users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake3@mail.com",
                Money = "1234",
                Name = "John Johnson",
                Password = "Pwd1",
                Phone = "1234568845",
                UserType = 2
            });
            users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake4@mail.com",
                Money = "1234",
                Name = "John Johnson",
                Password = "Pwd1",
                Phone = "1234568849",
                UserType = 2
            });
        }

        public static void InitializeUsersForm(this List<User> users)
        {
            users.Add(new User { Address = "Lila 123", Email = "lila@mail.com", IsActive = true, Money = 1234, Name = "Lilypooth", Password = Encoding.ASCII.GetBytes("Pwd1"), Phone = "12356", UserType = UserTypeEnum.SUPERUSER });
            users.Add(new User { Address = "Lila 123", Email = "lila2@mail.com", IsActive = true, Money = 1234, Name = "Lilypoothie", Password = Encoding.ASCII.GetBytes("Pwd1"), Phone = "1234566", UserType = UserTypeEnum.NORMAL });
            users.Add(new User { Address = "Lila 123", Email = "lila@mail.com", IsActive = true, Money = 1234, Name = "Lilypooth", Password = Encoding.ASCII.GetBytes("Pwd1"), Phone = "162317434", UserType = UserTypeEnum.PREMIUM });
            users.Add(new User { Address = "Lila 122435", Email = "lila4@mail.com", IsActive = true, Money = 1234, Name = "Lilypooth", Password = Encoding.ASCII.GetBytes("Pwd1"), Phone = "958345635", UserType = UserTypeEnum.PREMIUM });
        }
    }
}
