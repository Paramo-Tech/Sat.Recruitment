using Moq;
using Sat.Recruitment.Api.Cache.Interfaces;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Repository
{
    [CollectionDefinition("RepositoryTest", DisableParallelization = true)]
    public class UserRepositoryTest
    {
        //private Mock<ICacheService> cacheServiceMock;
        private IUserRepository _repository;
        private UserRepositoryMock mock;
        public UserRepositoryTest()
        {
            mock = new UserRepositoryMock();
            //cacheServiceMock = new Mock<ICacheService>();
            _repository = new UserRepository(mock.CacheServiceMock.Object);
        }

        #region IWritableRepository
        [Fact]
        public void Add_entity()
        {
            mock.SetupEmptyCacheServiceMock();
            var entity = new NormalUser
            {
                Name = "Agustin",
                Email = "Agustin@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124
            };

            _repository.Add(entity);
            var result =_repository.GetAll();

            Assert.Contains(entity, result);
        }

        [Fact]
        public void Add_range()
        {

            mock.SetupEmptyCacheServiceMock();

            List<IUser> userList = mock.UserList.ToList();


            _repository.AddRange(userList);

            var users = _repository.GetAll();


            Assert.Contains(userList[0], users);
            Assert.Contains(userList[1], users);
        }

        [Fact]
        public void Remove()
        {
            var userList = mock.UserList;
            mock.SetupFilledCacheServiceMock();

            _repository.Remove(userList.ElementAt(0));
            var result = _repository.GetAll();

            Assert.DoesNotContain(userList.ElementAt(0), result);
            Assert.Contains(userList.ElementAt(1), result);
        }

        [Fact]
        public void Update()
        {
            mock.SetupFilledCacheServiceMock();
            var userList = mock.UserList;
            var updatedUser = userList.ElementAt(1);
            updatedUser = new PremiumUser
            {
                Id = updatedUser.Id,
                Name = "Leonel",
                Email = "Leonel.Fernandez@gmail.com",
                Address = "Alem y Cerrito",
                Phone = "+534645213123",
                Money = 123456
            };
            _repository.Update(updatedUser);


            var result = _repository.GetAll();

            Assert.Contains(updatedUser, result);
            Assert.DoesNotContain(userList.ElementAt(1), result);
            Assert.Contains(userList.ElementAt(0), result);
        }

        #endregion

        #region IReadableRepository
        
        [Fact]
        public void Get()
        {
            mock.SetupFilledCacheServiceMock();
            var user = mock.UserList.FirstOrDefault(x => x.Id == 1);
            var result = _repository.Get(x => x.Id == 1);
            Assert.Equal(user, result);
        }

        [Fact]
        public void Get_all()
        {
            mock.SetupFilledCacheServiceMock();
            var userList = mock.UserList;
            var result = _repository.GetAll();
            Assert.Contains(userList.ElementAt(0), result);
            Assert.Contains(userList.ElementAt(1), result);
        }

        [Fact]
        public void Get_all_with_params()
        {
            mock.SetupFilledCacheServiceMock();
            var users = mock.UserList.Where(x => x.Id == 1);
            var result = _repository.GetAll(x=>x.Id == 1);
            Assert.Equal(users, result);
        }

        [Fact]
        public void Any()
        {
            mock.SetupFilledCacheServiceMock();
            var result = _repository.Any(x=>x.Id==1);
            Assert.True(result);
        }
        #endregion
    }
}
