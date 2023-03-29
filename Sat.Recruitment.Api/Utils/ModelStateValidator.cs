using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Sat.Recruitment.Api.Utils
{
    public static class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        FieldName = x.Key,
                        Error = x.Value.Errors.First().ErrorMessage
                    })
                    .Select(x => $@"[{x.FieldName}]: '{x.Error}'")
                    .ToList();

            var singleErrorLine = string.Join(" | ", errors);

            return new BadRequestObjectResult(Envelope.Error(singleErrorLine));
        }
    }
}
