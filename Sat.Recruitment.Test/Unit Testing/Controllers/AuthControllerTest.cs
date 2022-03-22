using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Unit_Testing.Controllers
{
    public class AuthControllerTest : ScenarioBase
    {
        private readonly AuthController _controller;
        private readonly Fake<IMediator> _mediator;

        public AuthControllerTest()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<AuthController>();
            _mediator = new Fake<IMediator>();
            var inMemorySettings = new Dictionary<string, string> {
                {"Key", "pintusharmaqqlhfdcvbnrelokqqohrdcqqqqqqqqqqqqweqwe"},                
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _controller = new AuthController(logger, _mediator.FakedObject, configuration);
        }

        [Fact]
        public async Task Login_Ok()
        {
            var okResult = (await _controller.Login(new LoginForm { Email = "Juan@marmol.com", Password = "Paswordd1" })) as ObjectResult;
            var authUser = okResult.Value as AuthenticatedUser;

            Assert.NotNull(authUser);
        }

        [Fact]
        public async Task Login_Fail()
        {
            await Assert.ThrowsAsync<Exception>(async() => await _controller.Login(null));

        }
    }
}
