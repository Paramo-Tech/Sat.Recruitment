using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Context;
using Sat.Recruitment.Api.Data.Repositories;
using Sat.Recruitment.Api.Handlers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Strategy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult> CreateUser([FromBody]User user)
        {
            try
            {
                user.ProcessNewUser();

                await _userRepository.Add(user);

                return Ok("User created successfully.");
            }
            catch (DbUpdateException ex)
            {
                var innerException = (SqliteException)ex.InnerException;
                if (innerException != null && innerException.SqliteErrorCode == 19)
                {
                    _logger.LogWarning("Cannot add duplicated user");
                    return BadRequest("This user is a duplicate and cannot be added.");
                }
                else
                {
                    _logger.LogWarning(innerException.Message);
                    return BadRequest(ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                return StatusCode(500, ex.InnerException.Message);
            }
        }

        // Added a simple Get() request to validate user creation
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_userRepository.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
