using System;
using System.Dynamic;
using DAL;
using Microsoft.AspNetCore.Mvc;
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
    public class UserServiceTest
    {
        private readonly IRepository<User, string> userRepository;
        private readonly IParser<User,string> userParser;
        private readonly IParser<UserType, string> userTypeParser;

        public UserServiceTest()
        {
            //default implementations, to be changed when the test case requires it
            var services = new ServiceCollection();
            services.AddScoped<IParser<User,string>, UserParser>();
            services.AddScoped<IParser<UserType, string>, UserTypeParser>();
            services.AddScoped<IRepository<User, string>, UserRepository>();

            var serviceProvider = services.BuildServiceProvider();

            userRepository = serviceProvider.GetService<IRepository<User, string>>();
            userParser = serviceProvider.GetService<IParser<User,string>>();
            userTypeParser = serviceProvider.GetService<IParser<UserType, string>>();
        }

        [Fact]
        public void UserExists_True()
        {
            var userService = new UserService(userRepository, userParser,userTypeParser);
            var user = userService.GetUsers()[0];
            var result = userService.UserExists(user);

            Assert.True(result);
        }

        [Fact]
        public void UserExists_False()
        {
            var userService = new UserService(userRepository, userParser, userTypeParser);
            var user = new User
            {
                Address = "stree 1",
                Email = "a@b.com",
                Money = new decimal(10),
                Name = "John Dow",
                Phone = "111111",
                UserType = userTypeParser.Parse("Normal")
            };
            var result = userService.UserExists(user);

            Assert.False(result);
        }
    }
}
