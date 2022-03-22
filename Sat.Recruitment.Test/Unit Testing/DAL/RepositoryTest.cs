using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Unit_Testing.DAL
{
    public class RepositoryTest : ScenarioBase
    {
        private readonly List<User> _users;

        public RepositoryTest()
        {
            _users = new List<User>();
            _users.InitializeUsersForm();
        }

        [Fact]
        public void Add_Ok()
        {
            var user = _repository.Add(_users[0]);

            Assert.NotNull(user);

            _repository.Delete(user.Id);
        }

        [Fact]
        public void Add_Fail()
        {
            var user = _repository.Add(_users[0]);
            Assert.Throws<ArgumentException>(() => _repository.Add(_users[0]));
            _repository.Delete(user.Id);
        }

        [Fact]
        public async Task AddAsync_Ok()
        {
            var user = await _repository.AddAsync(_users[0], CancellationToken.None);

            Assert.NotNull(user);

            await _repository.DeleteAsync(user.Id, CancellationToken.None);
        }

        [Fact]
        public async Task AddAsync_Fail()
        {
            var user = await _repository.AddAsync(_users[0], CancellationToken.None);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _repository.AddAsync(_users[0], CancellationToken.None));

            await _repository.DeleteAsync(user.Id, CancellationToken.None);
        }

        [Fact]
        public void Delete_Ok()
        {
            var user = _repository.Add(new User { Address = "asdadg", Email= "dgsdfg", IsActive = true, Money = 12423, Name = "gdrhrtyrt", Password = Encoding.ASCII.GetBytes("gsdlkhjfdghj"), Phone = "454635", UserType = UserTypeEnum.SUPERUSER});
            _repository.Delete(user.Id);
            var deletedUser =_repository.Get(user.Id);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task GetAllAsync_Ok()
        {
            var users = await _repository.GetAllAsync(CancellationToken.None);
            Assert.True(users.Count > 0);
        }

        [Fact]
        public async Task GetAsync_Ok()
        {
            var user = await _repository.GetAsync(1, CancellationToken.None);
            Assert.NotNull(user);
        }

        [Fact]
        public void Update_Ok()
        {
            var user = _users.FirstOrDefault();
            _repository.Add(user);
            string newAddress = "Uruguay 12435";
            
            user.Address = newAddress;
            var modifiedUser = _repository.Update(user);
            Assert.Equal(newAddress, modifiedUser.Address);
            _repository.Delete(user.Id);
        }

        [Fact]
        public async Task UpdateAsync_Ok()
        {
            var user = _users.FirstOrDefault();
            await _repository.AddAsync(user, CancellationToken.None);
            string newAddress = "Uruguay 12435";
            user.Address = newAddress;
            var modifiedUser = await _repository.UpdateAsync(user,CancellationToken.None);
            Assert.Equal(newAddress, modifiedUser.Address);
            await _repository.DeleteAsync(user.Id, CancellationToken.None);
        }

        [Fact]
        public void AddRange_Ok()
        {
            var users = CheckInsertedUsers(_repository.GetAll(), _users);
            _repository.AddRange(users);
            Assert.NotNull(_repository.GetAll().FirstOrDefault(x => x.Email.Equals(users.FirstOrDefault().Email)));
            _repository.DeleteRange(users);
        }

        [Fact]
        public async Task AddRangeAsync_Ok()
        {
            var users = CheckInsertedUsers(await _repository.GetAllAsync(CancellationToken.None), _users);
            await _repository.AddRangeAsync(users, CancellationToken.None);
            Assert.NotNull(_repository.GetAll().FirstOrDefault(x => x.Email.Equals(users.FirstOrDefault().Email)));
            await _repository.DeleteRangeAsync(users, CancellationToken.None);
        }

        private List<User> CheckInsertedUsers(List<User> insertedUsers, List<User> usersToInsert)
        {
            var usersReady = new List<User>();
            foreach (var userToInsert in usersToInsert)
            {
                if(insertedUsers.FirstOrDefault(x => x.Email.Equals(userToInsert.Email)) == null
                    && insertedUsers.FirstOrDefault(x => x.Phone.Equals(userToInsert.Email)) == null)
                    usersReady.Add(userToInsert);
            }
            return usersReady;
        }
    }
}
