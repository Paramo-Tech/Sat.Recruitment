using Domain;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Sat.Recruitment.API.Extentions
{
    [ExcludeFromCodeCoverage]
    internal static class ResultExtension
    {
        internal static IResult ResultResponse<T>(this IResultExtensions resultExtensions, T data, string message = null, bool isSuccess = true)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions, nameof(resultExtensions));
            return new Result<T>(data, message, isSuccess);
        }

        class Result<T> : IResult
        {
            public Result(T data, string message, bool isSuccess)
            {
                this.Data = data;
                this.Message = message;
                this.IsSuccess = isSuccess;
            }

            public T Data { get; private set; }
            public string Message { get; }
            public bool IsSuccess { get; }

            public Task ExecuteAsync(HttpContext httpContext)
            {
                string content = JsonSerializer.Serialize(this);
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(content);
                switch (httpContext.Request.Method)
                {
                    case "GET":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        break;
                    case "POST":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                        break;
                    default:
                        break;
                }
                return httpContext.Response.WriteAsync(content);
            }
  


        }
    }
}
