namespace Sat.Recruitment.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) :base(message) 
        {
            Code = 404;
        }
    }
}
