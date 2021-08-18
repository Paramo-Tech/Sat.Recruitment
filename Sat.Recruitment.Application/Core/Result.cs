
namespace Sat.Recruitment.Application.Core
{
    /// <summary>
    /// generic class to control api responses 
    /// </summary>
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Value { get; set; }
        public string Error { get; set; }

        public static Result<T> Success(string value) => new Result<T> { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }
}
