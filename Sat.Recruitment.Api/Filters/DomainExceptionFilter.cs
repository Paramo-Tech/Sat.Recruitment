using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sat.Recruitment.Domain.Exceptions;

namespace Sat.Recruitment.Api.Filters
{
    public sealed class DomainExceptionFilter : IExceptionFilter // IActionFilter // 
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is null)// or not DomainException)
            {
                return;
            }
            var exception = context.Exception;// as DomainException;
            var attribute = context.ActionDescriptor.FilterDescriptors
            .Select(x => x.Filter).OfType<DomainExceptionMapperAttribute>().Where(a => a.ExceptionTypeName == exception?.GetType().Name).FirstOrDefault();

            if (attribute != null)
            {
                var httpStatusCode = (int)attribute.HttpStatusCode;
                //var errorCode = exception?.ErrorCode;
                var errorCode = exception is DomainException domainException ? domainException.ErrorCode : null;
                var error = new ApiErrorResponse() { ErrorKey = errorCode ?? "no_error_code_defined", ErrorDescription = context.Exception.Message, HttpStatusCode = httpStatusCode };
                context.Result = new JsonResult(error)
                {
                    StatusCode = httpStatusCode
                };
                // context.ExceptionHandled = true;
                return;
            }
            throw new KeyNotFoundException($"There is no http status code mapped for {context.Exception} domain exception");
        }
    }
}

