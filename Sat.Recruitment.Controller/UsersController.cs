using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Sat.Recruitment.DTOs;
using Sat.Recruitment.Entities.Exceptions;
using Sat.Recruitment.Presenter;
using Sat.Recruitment.UseCasesAbstractions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sat.Recruitment.Controller
{

    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly ICarpoolingService _carpoolingService;
        private readonly IPostUserInputPort _userInputPort;
        private readonly IPostUserOutputPort _userOutputPort;


        public  UsersController(IPostUserInputPort postUserInputPort, IPostUserOutputPort postUserOutputPort)
        {
            _userInputPort = postUserInputPort;
            _userOutputPort = postUserOutputPort;

        }

        [HttpPost("/create-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            try
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(userDto);

                bool isValid = Validator.TryValidateObject(userDto, context, results, true);

                if (!isValid)
                {
                    string errors = string.Join("; ", results.Select(r => r.ErrorMessage));
                    return BadRequest(new Result { IsSuccess = false, Errors = errors });
                }
                await _userInputPort.Handle(userDto);

                return Ok(((IPresenter<Result>)_userOutputPort).Content);
            }
            catch (DuplicatedUserException)
            {
                Debug.WriteLine("The user is duplicated");
                return Conflict(new Result
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                });
            }
            catch (Exception)
            {
                return Conflict(new Result
                {
                    IsSuccess = false,
                    Errors = "An error occurred while processing the user"
                });
            }
        }
    }
}