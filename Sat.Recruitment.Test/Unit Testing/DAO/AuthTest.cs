using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Services.Authentication.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Unit_Testing.DAO
{
    public class AuthTest : ScenarioBase
    {
        [Fact]
        public async Task Handle_Login_Ok()
        {
            
            var request = new LoginCommand("test1@mail.com", "Pwd1", "pintusharmaqqlhfdcvbnrelokqqohrdcqqqqqqqqqqqqweqwe");

            var handler = new LoginHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_Login_Fail()
        {

            var request = new LoginCommand("test2@mail.com", "Pwd1", "pintusharmaqqlhfdcvbnrelokqqohrdcqqqqqqqqqqqqweqwe");

            var handler = new LoginHandler(_context);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
