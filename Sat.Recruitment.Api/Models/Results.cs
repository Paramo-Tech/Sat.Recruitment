namespace Sat.Recruitment.Api.Models
{
    public class Result
    {
        public Result()
        {
            IsSuccess = false;
            Errors = string.Empty;
        }
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
