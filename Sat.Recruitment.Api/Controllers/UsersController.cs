using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Dtos;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Commands;
using Sat.Recruitment.Api.Services.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IQueriesUserService queryService;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UsersController(ILogger<UsersController> logger, IQueriesUserService queryService,
            IMediator mediator, IMapper mapper)
        {
            this.logger = logger;
            this.queryService = queryService;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("/")]
        public string Index()
        {
            return "Running";
        }

        [HttpGet("all")]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await queryService.GetAllasync();
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserCommand createCommand)
        {
            var createUserValidator = new CreateUserValidator();
            var validResult = await createUserValidator.ValidateAsync(createCommand);
            
            if (!validResult.IsValid)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = new StringBuilder().AppendJoin(" ", validResult.Errors?.Select(e => e.ErrorMessage).ToArray()).ToString()
                };
            }
            
            var _users = mapper.Map<List<User>>(await queryService.GetAllasync());
            var userValidator = new UserValidator(_users);
            try
            {
                var createCommandUser = mapper.Map<User>(createCommand);
                var isDuplicated = userValidator.IsDuplicated(createCommandUser);

                switch (isDuplicated)
                {
                    case true:
                        throw new Exception("User is duplicated");
                    default:
                        await mediator.Send(createCommand);
                        logger.LogDebug("User Created");
                        return new Result()
                        {
                            IsSuccess = true,
                            Errors = "User Created"
                        };
                }
            }
            catch(Exception ex)
            {
                
                logger.LogDebug($"Error on creating user: {ex.Message}");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }
    }
}
