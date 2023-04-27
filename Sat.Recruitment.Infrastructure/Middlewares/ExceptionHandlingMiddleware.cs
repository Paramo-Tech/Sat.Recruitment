using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sat.Recruitment.Application.Common.Models;
using System.Net;

namespace Sat.Recruitment.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var errorresponse = new Result
            {
                IsSuccess = false,
            };

            switch (exception)
            {
                case ApplicationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorresponse.Message = ex.Message;
                    break;
                case ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ObjectResult<IEnumerable<string>> result = new ObjectResult<IEnumerable<string>>();
                    result.Result = ex.Errors.Select(m => m.ErrorMessage);
                    errorresponse = result;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorresponse.Message = exception.Message;
                    break;
            }

            _logger.LogError(exception.Message);

            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(errorresponse));
        }
    }
}
