namespace Sat.Recruitment.Api.Middlewares
{
    public class ErrorResponse
    {
        public ErrorResponse(string message) 
        {
            this.Message = message;
        }
    
        public string Message { get; private set; }
    }
}
