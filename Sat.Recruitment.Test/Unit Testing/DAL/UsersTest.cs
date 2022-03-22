using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Services.Users.Commands;
using Sat.Recruitment.Services.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Unit_Testing.DAL
{
    public class UsersTest : ScenarioBase
    {
        private readonly List<UserCreationForm> _users;
        public UsersTest()
        {
            _users = new List<UserCreationForm>();
            _users.InitializeTestUsersCreationFormList();
        }
        [Fact]
        public async Task Get_User_Ok()
        {
            var request = new GetUserQuery(1);

            var handler = new GetUserHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_User_Null()
        {
            var request = new GetUserQuery(59080);

            var handler = new GetUserHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Get_Users_MoreThan0()
        {
            var request = new GetAllUsersQuery();

            var handler = new GetAllUsersHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task Get_Users_NotEqual30()
        {
            var request = new GetAllUsersQuery();

            var handler = new GetAllUsersHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEqual(30, result.Count);
        }

        [Fact]
        public async void Get_ActiveUsers_Equal2()
        {
            var request = new GetAllActiveUsersQuery();

            var handler = new GetAllActiveUsersHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task Get_ActiveUsers_NotEqual20()
        {
            var request = new GetAllActiveUsersQuery();

            var handler = new GetAllActiveUsersHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotEqual(20, result.Count);
        }

        [Fact]
        public async Task Insert_User_Ok()
        {
            var request = new CreateUserCommand(_users[0]);

            var handler = new CreateUserHandler(_repository);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Insert_User_DuplicateEmailFail()
        {
            var handler = new CreateUserHandler(_repository);

            var request = new CreateUserCommand(_users[1]);

            var result =  await handler.Handle(request, CancellationToken.None);

            var request2 = new CreateUserCommand(_users[2]);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(request2, CancellationToken.None));

            await _repository.DeleteAsync(result.Id,CancellationToken.None);
        }

        [Fact]
        public async Task Insert_User_DuplicatePhoneFail()
        {
            var handler = new CreateUserHandler(_repository);

            var request = new CreateUserCommand(_users[2]);

            var result = await handler.Handle(request, CancellationToken.None);

            var request2 = new CreateUserCommand(_users[3]);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(request2, CancellationToken.None));

            await _repository.DeleteAsync(result.Id, CancellationToken.None);
        }       

        [Fact]
        public async Task Edit_User_Ok()
        {
            string newAddress = "Uruguay 1343";
            var form = new UserEditionForm
            {
                Id = 1,
                Address = newAddress
            };

            var query = new EditUserCommand(form);
            var handler = new EditUserHandler(_repository);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(newAddress, result.Address);
        }


        [Fact]
        public async Task Edit_User_Fail()
        {
            string newAddress = "Uruguay 1343";
            var form = new UserEditionForm
            {
                Id = 100,
                Address = newAddress
            };

            var query = new EditUserCommand(form);
            var handler = new EditUserHandler(_repository);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_User_Ok()
        {

            var query = new DeleteUserCommand(1);
            var handler = new DeleteUserHandler(_repository);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsActive);
        }

        [Fact]
        public async Task Delete_User_Fail()
        {

            var query = new DeleteUserCommand(100);
            var handler = new DeleteUserHandler(_repository);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
