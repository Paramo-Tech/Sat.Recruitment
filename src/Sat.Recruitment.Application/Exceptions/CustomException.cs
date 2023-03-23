namespace Sat.Recruitment.Application.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; }

        public CustomException(string message) : base(message)
        {
            
        }
    }
}
