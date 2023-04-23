using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using Serilog;

namespace Sat.Recruitment.Api.Helpers
{

    

    public class ErrorLoggingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, "Unhandled exception");

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(new { message = "An unexpected error occurred. Please try again later." });
            context.ExceptionHandled = true;
        }
    }


}
