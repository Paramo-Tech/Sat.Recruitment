using System;
using System.Dynamic;
using DAL;

using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Parsers;
using Sat.Recruitment.Business.Types;
using Sat.Recruitment.DAL.Interfaces;
using Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {

        private readonly UsersController userController;
        private readonly IRepository<User, string> userRepository;
        private readonly IParser<User, string> userParser;
        private readonly IParser<UserType, string> userTypeParser;

        public UserControllerTest()
        {
            //default implementations, to be changed when the test case requires it
            var services = new ServiceCollection();
            services.AddScoped<IParser<User, string>, UserParser>();
            services.AddScoped<IParser<UserType, string>, UserTypeParser>();
            services.AddScoped<IRepository<User, string>, UserRepository>();

            var serviceProvider = services.BuildServiceProvider();

            userRepository = serviceProvider.GetService<IRepository<User,string>>();
            userParser = serviceProvider.GetService<IParser<User,string>>();
            userTypeParser = serviceProvider.GetService<IParser<UserType, string>>();
            userController = new UsersController(userRepository, userParser,userTypeParser);

        }

        [Fact]
        public void UserCreation_success()
        {
            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created.", result.Errors);
        }

        [Fact]
        public void UserCreation_failed_user_duplicated()
        {

            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Garay y Otra Calle", "+534645213542", "SuperUser", "112234").Result;
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated.", result.Errors);
        }
    }
}
