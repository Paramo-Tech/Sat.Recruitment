using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Core.ResponsesExceptions;

namespace Sat.Recruitment.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadRequestException badRequest)
            {
                context.Result = new ObjectResult(badRequest.ErrorMessage)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is NotFoundException notFound)
            {
                context.Result = new ObjectResult(notFound.ErrorMessage)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            context.ExceptionHandled = true;

            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

        }
    }
}


