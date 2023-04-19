using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Responses;
using Sat.Recruitment.Core.DTOs;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly string _success = "User Created";
        private readonly List<UserDto> _UserDtos = new List<UserDto>();
        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-User")]
        public async Task<Result<UserDto>> CreateUser(UserDto request)
        {

            try
            {
                var user = this._mapper.Map<USER>(request);
                var result = await this._userService.Create(user);
                if (result != _success)
                    return new Result<UserDto>(null)
                    {
                        IsSuccess = false,
                        Message = "The User is duplicated"
                    };
            }
            catch
            {
                Debug.WriteLine("The User is duplicated");
                return new Result<UserDto>(null)
                {
                    IsSuccess = false,
                    Message = "The User is duplicated"
                };
            }

            return new Result<UserDto>(null)
            {
                IsSuccess = true,
                Message = "User Created"
            };
        }


    }
}
