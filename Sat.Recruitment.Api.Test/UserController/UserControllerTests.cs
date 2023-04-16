using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business.Contracts;
using Sat.Recruitment.Business.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserControllerTests
    {
        public Mock<IUserService> MockService { get; set; }
        public UsersController Controller { get; set; }
        public UserControllerTests()
        {
            MockService = new Mock<IUserService>();
            Controller = new UsersController(MockService.Object);
        }

    }
}
