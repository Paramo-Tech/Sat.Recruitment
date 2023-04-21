using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Token.Models.DTOs;
using Sat.Recruitment.Token.Models.Validators;
using Sat.Recruitment.Token.Services;
using System.Collections.Generic;
using System.Net;

namespace Sat.Recruitment.Token.Controllers
{
    public class AccountAPIController : ControllerBase
    {
        private readonly AccountService _accountService;
        protected ResponseDto _response;

        public AccountAPIController(AccountService accountService)
        {
            _response = new ResponseDto();
            _accountService = accountService;
        }

        [HttpPost]
        [Route("v1/api/account/token")]
        public ResponseDto GetToken([FromBody] AccountDto accountDto)
        {
            var _validator = new AccountDtoValidator();
            var result = _validator.Validate(accountDto);

            if (!result.IsValid)
            {
                _response.Status = (int)HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = _validator.ConvertFailuresToErrorMessages(result.Errors);
                
                return _response;
            }

            string token = _accountService.Authenticate(accountDto);
            if (!string.IsNullOrEmpty(token))
            {
                _response.Result = token;
                _response.DisplayMessage = Constants.ValidationMessage.ValidJwtToken;

                return _response;
            }

            _response.Status = (int)HttpStatusCode.Unauthorized;

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>()
                    {
                        Constants.ValidationMessage.InvalidJwtToken
                    };

            return _response;
        }
    }
}
