using Moq;
using Sat.Recruitment.Api.Cache.Interfaces;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.Repository
{
    public class UserRepositoryMock
    {
        private Mock<ICacheService> _cacheServiceMock;
        public UserRepositoryMock()
        {
            this._cacheServiceMock = new Mock<ICacheService>();
        }
        private IEnumerable<IUser> _userList = new List<IUser>() {
                new NormalUser
                {
                    Id = 1,
                    Name = "Agustina",
                    Email = "Agustina@gmail.com",
                    Address = "Av. Juan G",
                    Phone = "+349 1122354215",
                    Money = 124
                },
                new PremiumUser
                {
                    Id=2,
                    Name = "Franco",
                    Email = "Franco.Perez@gmail.com",
                    Address = "Alvear y Colombres",
                    Phone = "+534645213542",
                    Money = 112234
                }
        };

        public IEnumerable<IUser> UserList { get => _userList.ToList(); }
        public Mock<ICacheService> CacheServiceMock { get => _cacheServiceMock; set => _cacheServiceMock = value; }

        public void SetupFilledCacheServiceMock()
        {
            var userList = UserList;
            _cacheServiceMock.Setup(x => x.TryGet("Users", out userList)).Returns(true);

        }
        public void SetupEmptyCacheServiceMock()
        {
            IEnumerable<IUser> userList = new List<IUser>() {
                new NormalUser
                {
                    Id = 1,
                    Name = "name",
                    Email = "Email",
                    Address = "Address",
                    Phone = "Phone",
                    Money = 0
                }
            };
            _cacheServiceMock.Setup(x => x.TryGet("Users", out userList)).Returns(true);

        }
    }
}
