using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public partial class UsersController : ApiControllerBase
    {
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
