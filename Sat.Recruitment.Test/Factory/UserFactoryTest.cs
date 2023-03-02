using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Factory;
using Sat.Recruitment.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Factory
{
    [CollectionDefinition("FactoryTest", DisableParallelization = true)]
    public class UserFactoryTest
    {
        private UserDTO Dto => new UserDTO()
        {
            Name = "Agustina",
            Email = "Agustina@gmail.com",
            Address = "Av. Juan G",
            Phone = "+349 1122354215",
            Money = 124
        };
        private readonly UserFactory _factory;
        public UserFactoryTest()
        {
            _factory=new UserFactory();
        }

        [Fact]
        public void Create_normal_user()
        {
            var userDto = Dto;
            userDto.UserType = UserTypes.NORMAL;
            var result = _factory.CreateUser(userDto);

            Assert.IsType<NormalUser>(result);
            Assert.Equal(userDto.Name, result.Name);
            Assert.Equal(userDto.Email, result.Email);
            Assert.Equal(userDto.Address, result.Address);
            Assert.Equal(userDto.Phone, result.Phone);
            Assert.Equal(userDto.Money, result.Money);
        }


        [Fact]
        public void Create_super_user()
        {
            var userDto = Dto;
            userDto.UserType = UserTypes.SUPER;
            var result = _factory.CreateUser(userDto);

            Assert.IsType<SuperUser>(result);
            Assert.Equal(userDto.Name, result.Name);
            Assert.Equal(userDto.Email, result.Email);
            Assert.Equal(userDto.Address, result.Address);
            Assert.Equal(userDto.Phone, result.Phone);
            Assert.Equal(userDto.Money, result.Money);
        }


        [Fact]
        public void Create_premium_user()
        {
            var userDto = Dto;
            userDto.UserType = UserTypes.PREMIUM;
            var result = _factory.CreateUser(userDto);

            Assert.IsType<PremiumUser>(result);
            Assert.Equal(userDto.Name, result.Name);
            Assert.Equal(userDto.Email, result.Email);
            Assert.Equal(userDto.Address, result.Address);
            Assert.Equal(userDto.Phone, result.Phone);
            Assert.Equal(userDto.Money, result.Money);
        }

        [Fact]
        public void Create_invalid_usertype()
        {

            var dto = Dto;
            dto.UserType = (UserTypes)100;

            var user = _factory.CreateUser(dto);

            Assert.Null(user);
        }
    }
}
