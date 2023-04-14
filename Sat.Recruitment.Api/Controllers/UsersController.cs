using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Contracts.Application;
using Sat.Recruitment.Application.Dto;
using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("/create-user")]
        public ResultUser CreateUser([FromBody] UserDto userDto)
        {
            return _userService.CreateUser(_mapper.Map<User>(userDto));
        }
    }

}

