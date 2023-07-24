namespace Sat.Recruitment.Api.Helpers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public int codeStatus { get; set; }
    }
}
