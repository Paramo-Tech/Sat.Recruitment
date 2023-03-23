namespace Sat.Recruitment.Application.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException(string message) : base(message)
        {
            Code = 400;
        }
    }
}
