namespace Sat.Recruitment.Domain.Results
{
    public class ExecutionResult
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; } = string.Empty;
    }
}
