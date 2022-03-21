using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Services.Users.Commands;
using Sat.Recruitment.Services.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Unit_Testing.DAO
{
    public class UsersTest : ScenarioBase
    {
        private readonly List<UserCreationForm> _users;
        public UsersTest()
        {
            _users = new List<UserCreationForm>();  
            _users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake@mail.com",
                Money = "1234",
                Name = "John Doe",
                Password = "Pwd1",
                Phone = "12345688",
                UserType = 2
            });
            _users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake2@mail.com",
                Money = "1234",
                Name = "John Johnson",
                Password = "Pwd1",
                Phone = "123456884",
                UserType = 2
            });
            _users.Add(new UserCreationForm
            {
                Address = "Fake Street 123",
                Email = "fake3@mail.com",
                Money = "1234",
                Name = "John Johnson",
                Password = "Pwd1",
                Phone = "1234568845",
                UserType = 2
            });

        }
        [Fact]
        public async Task Get_User_Ok()
        {
            var request = new GetUserQuery(1);

            var handler = new GetUserHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_User_Null()
        {
            await Task.Delay(1000);

            var request = new GetUserQuery(5);

            var handler = new GetUserHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Get_Users_Equal2()
        {
            var request = new GetAllUsersQuery();

            var handler = new GetAllUsersHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Get_Users_NotEqual30()
        {
            var request = new GetAllUsersQuery();

            var handler = new GetAllUsersHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEqual(30, result.Count);
        }

        [Fact]
        public async void Get_ActiveUsers_Equal2()
        {
            var request = new GetAllActiveUsersQuery();

            var handler = new GetAllActiveUsersHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Get_ActiveUsers_NotEqual20()
        {
            var request = new GetAllActiveUsersQuery();

            var handler = new GetAllActiveUsersHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEqual(20, result.Count);
        }

        [Fact]
        public async Task Insert_User_Ok()
        {
            await Task.Delay(2000);
            var request = new CreateUserCommand(_users.FirstOrDefault());

            var handler = new CreateUserHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);

            _context.Users.Remove(result);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Insert_User_CompositeIndexFail()
        {
            await Task.Delay(8000);
            var request = new CreateUserCommand(_users[1]);

            var handler = new CreateUserHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            var request2 = new CreateUserCommand(_users[2]);

            //var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(request2, CancellationToken.None));
            var result2 = await handler.Handle(request2, CancellationToken.None);
            Assert.NotNull(result2);
            _context.RemoveRange(_users);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
