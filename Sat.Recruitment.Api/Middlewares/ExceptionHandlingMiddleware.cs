using Microsoft.AspNetCore.Http;
using Shared.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationException = Shared.Domain.Exceptions.ApplicationException;

namespace Sat.Recruitment.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    Success = false
                };

                switch (error)
                {
                    case ApplicationException or DomainException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = error.Message;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Message = "Internal Server errors.";
                        break;
                }

                var result = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
