using AutoMapper;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Forms;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Unit_Testing.Controllers
{
    public class UsersControllerTest : ScenarioBase
    {
        private readonly UsersController _controller;
        private readonly Fake<IMediator> _mediator;
        private readonly Fake<IMapper> _mapper;

        public UsersControllerTest()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<UsersController>();
            _mediator = new Fake<IMediator>();
            _mapper = new Fake<IMapper>();
            _controller = new UsersController(logger, _mediator.FakedObject, _mapper.FakedObject);
        }

        [Fact]
        public async Task GetUser_Ok()
        {
            var result = await _controller.Get(1);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task GetUsers_Ok()
        {
            var objResult = await _controller.Get();
            Assert.NotNull(objResult);
        }

        [Fact]
        public async Task GetActiveUsers_Ok()
        {
            var objResult = await _controller.GetActiveUsers();
            Assert.NotNull(objResult);
        }

        [Fact]
        public async Task CreateUsers_Ok()
        {
            var objResult = await _controller.CreateUser(new UserCreationForm { Address = "sdfg", Email = "ds", Money = "123", Name = "sadgsdfg", Password = "2sdg", Phone = "23124", UserType = 1 });
            Assert.NotNull(objResult);
        }

        [Fact]
        public async Task EditUser_Ok()
        {
            var objResult = await _controller.EditUser(new UserEditionForm { Address = "sdfg", Password = "2sdg", UserType = 1 });
            Assert.NotNull(objResult);
        }


        [Fact]
        public async Task DeleteUser_Ok()
        {
            var objResult = await _controller.DeleteUser(1);
            Assert.NotNull(objResult);
        }
    }
}
