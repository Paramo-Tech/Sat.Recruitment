using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MongoDB.Driver.Core.Operations;
using Sat.Recruitment.Api.Response;
using Sat.Recruitment.Core.DataTransferObjects;
using Sat.Recruitment.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Middlewares
{

    /// <summary>
    /// Calss to exception handling middleware
    /// </summary>
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke exception async
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="errorFactory"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, IErrorFactory errorFactory)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, errorFactory);
            }
        }

        /// <summary>
        /// Handle the exception
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="ex"></param>
        /// <param name="errorFactory"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, IErrorFactory errorFactory)
        {
            httpContext.Response.ContentType = "application/json";
            HttpResponse httpResponse = httpContext.Response;
            Tuple<string, int> result = ManageError(errorFactory, httpContext.Request.Method, ex);
            httpResponse.StatusCode = result.Item2;
            await httpContext.Response.WriteAsync(result.Item1);
        }

        /// <summary>
        /// Manage the error
        /// </summary>
        /// <param name="errorFactory"></param>
        /// <param name="method"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Tuple<string, int> ManageError(IErrorFactory errorFactory, string method, Exception ex)
        {
            IResponseResult<string> result = errorFactory.CreateError(method, ex);
            string item = JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return new Tuple<string, int> ( item, result.Operation.Status );
        }
    }
}
