namespace Sat.Recruitment.Global.WebContracts
{
    public class UserResult
    {
        public UserResult(bool isSuccess, string errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}