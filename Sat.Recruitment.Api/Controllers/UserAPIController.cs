using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models.DTOs;
using Sat.Recruitment.Api.Models.Validators;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [Authorize]
    public class UserAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly UserService _userService;

        public UserAPIController(UserService userService)
        {
            _response = new ResponseDto();
            _userService = userService;
        }

        [HttpGet]
        [Route("v2/api/users")]
        public async Task<ResponseDto> GetUsers()
        {
            try
            {
                IEnumerable<UserModelDto> userDto = await _userService.GetUsers();
                _response.Result = userDto;
            }
            catch (Exception ex)
            {
                _response.Status = (int)HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpGet]
        [Route("v2/api/users/types")]
        public async Task<ResponseDto> GetUsersTypes()
        {
            try
            {
                IEnumerable<UserTypeModelDto> userDto = await _userService.GetUsersTypes();
                _response.Result = userDto;
            }
            catch (Exception ex)
            {
                _response.Status = (int)HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpGet]
        [Route("v2/api/{userId}")]
        public async Task<object> GetUser(Guid userId)
        {
            try
            {
                UserModelDto userDto = await _userService.GetUserById(userId);
                _response.Result = userDto;
            }
            catch (Exception ex)
            {
                _response.Status = (int)HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _response;
        }

        [HttpPost]
        [Route("v2/api/user")]
        public async Task<object> Post([FromBody] UserModelDto userDto)
        {
            try
            {
                var _validator = new UserModelDtoValidator();
                var result = _validator.Validate(userDto);

                if (!result.IsValid)
                {
                    _response.Status = (int)HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = _validator.ConvertFailuresToErrorMessages(result.Errors);

                    return _response;
                }

                bool isRegister = await _userService.IsUserDuplicate(userDto);

                if (!isRegister)
                {
                    UserModelDto model = await _userService.SaveUpdateUser(userDto);
                    _response.Result = model;
                    _response.DisplayMessage = Constants.ValidationMessage.UserCreateMessage;

                    return _response;
                }

                _response.Status = (int)HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    Constants.ValidationMessage.DuplicateMessage
                };

            }
            catch (Exception ex)
            {
                _response.Status = (int)HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _response;
        }

        [HttpPut]
        [Route("v2/api/user")]
        public async Task<object> Put([FromBody] UserModelDto userDto)
        {
            try
            {
                var _validator = new UserModelDtoValidator();
                var result = _validator.Validate(userDto);

                if (!result.IsValid)
                {
                    _response.Status = (int)HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = _validator.ConvertFailuresToErrorMessages(result.Errors);

                    return _response;
                }

                UserModelDto model = await _userService.SaveUpdateUser(userDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.Status = (int)HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _response;
        }

        [HttpDelete]
        [Route("v2/api/user/{userId}")]

        public async Task<object> Delete(Guid userId)
        {
            try
            {
                bool isSuccess = await _userService.DeleteUser(userId);
                _response.Result = isSuccess;
                _response.DisplayMessage = Constants.ValidationMessage.UserDeleteMessage;
            }
            catch (Exception ex)
            {
                _response.Status = (int)HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _response;
        }
    }
}
