using System.Net;

namespace Sat.Recruitment.Application.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            CustomException ex;

            try
            {
                ex = (CustomException)exception;
            }
            catch (Exception)
            {
                ex = new CustomException(exception.Message);
                ex.Code = 500;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode =ex.Code;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
    }
}
