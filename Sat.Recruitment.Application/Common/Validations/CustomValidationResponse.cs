using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Common.Models;

namespace Sat.Recruitment.Application.Common.Validations
{
    public static class CustomValidationResponse
    {
        public static IActionResult MakeValidationResponse(ActionContext context)
        {
            return new OkObjectResult(new Result
            {
                IsSuccess = false,
                Errors = string.Join(" ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }
    }
}
