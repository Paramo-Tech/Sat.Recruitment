using System.Text;
using System.Threading.Tasks;
using Core.CQS.Commands;
using Infraestructure.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        readonly IMediator mediator;

        public UsersController(IMediator _mediatr)
        {
            mediator = _mediatr;
        }

        [HttpPost]
        public async Task<CreateNewUserResponse> CreateUser(CreateNewUserDTO dto)
        {
            CreateNewUserValidator validator = new CreateNewUserValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                // -- it should be 400(bad params)
                StringBuilder errors = new StringBuilder();
                validationResult.Errors.ForEach(x => errors.Append($" {x.ErrorMessage}"));
                return new CreateNewUserResponse()
                {
                    IsSuccess = false,
                    Errors = errors.ToString()
                };
            }

            var result = await mediator.Send<CreateNewUserResponse>(dto, cancellationToken: default);
            return result;
        }
    }
}
