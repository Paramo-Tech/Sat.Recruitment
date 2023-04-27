using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Services;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Infrastructure.Persistence;
using Sat.Recruitment.Infrastructure.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUserTest
    {
        [Fact]
        public async Task CreateUser()
        {
            var mapper = new Mock<IMapper>();
            var dateTimeProvider = new DateTimeProvider();
            var moneyCalculatorService = new MoneyCalculatorService();
            var emailHelper = new EmailHelper();

            var options = new DbContextOptionsBuilder<SatDbContext>()
           .UseInMemoryDatabase(databaseName: "SatDb")
           .Options;

            var dummyUser = new User();
            dummyUser.Email = "email@mail.com";
            dummyUser.UserType = "Normal";
            dummyUser.Address = "Dummy";
            dummyUser.Name = "Name";
            dummyUser.Phone = "123";

            mapper.Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>())).Returns(dummyUser);

            using (var context = new SatDbContext(options))
            {
                CreateUserHandler handler = new CreateUserHandler(context, dateTimeProvider, mapper.Object, moneyCalculatorService, emailHelper);
                CancellationTokenSource source = new CancellationTokenSource();

                var command = Mock.Of<CreateUserCommand>();

                var result = await handler.Handle(command, source.Token);

                Assert.Equal("User Created", result.Message);
                Assert.True(result.IsSuccess);
            }
        }
    }
}
