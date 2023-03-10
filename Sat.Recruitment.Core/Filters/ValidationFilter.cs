using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Domain.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace Sat.Recruitment.Web.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var paramObject = context.ActionArguments.SingleOrDefault(x => x.Value is IValidableDto).Value;

            if (paramObject != null)
            {
                IValidableDto paramDto = (IValidableDto)paramObject;

                var errors = await paramDto.ValidateDto();

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    context.Result = new BadRequestObjectResult(errors);
                    return;
                }
            }
            await next();
        }
    }
}
