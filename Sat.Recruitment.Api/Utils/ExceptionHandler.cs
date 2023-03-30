using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Utils
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
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
                _logger.LogError(ex, "Unhandled error.");

                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            var error = "Unhandled error";
#if DEBUG
            error = GetInnerMessageInException(ex);
#endif
            var result = JsonSerializer.Serialize(
                Envelope.Error(error),
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }

        private static string GetInnerMessageInException(Exception ex)
        {
            var lastMessage = ex.Message;
            var innerException = ex.InnerException;

            while (innerException != null)
            {
                lastMessage += "-->" + innerException.Message;
                innerException = innerException.InnerException;
            }

            return lastMessage;
        }
    }
}
