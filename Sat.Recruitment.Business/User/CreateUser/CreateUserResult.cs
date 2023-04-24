
namespace Sat.Recruitment.Business.User.CreateUser
{
    public class CreateUserResult
    {
        public CreateUserResult(bool success, string message)
        {
            Success = success;
            Errors = message;
        }

        public bool Success { get; set; }
        public string Errors { get; set; }
    }
}
