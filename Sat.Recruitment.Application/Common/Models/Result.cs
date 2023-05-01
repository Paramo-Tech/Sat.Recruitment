namespace Sat.Recruitment.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; private init; }
    public string Message { get; private init; } = string.Empty;

    public static Result Success(string message = "")
    {
        return new Result()
        {
            Message = message,
            IsSuccess = true
        };
    }

    public static Result Error(string errorMessage)
    {
        return new Result()
        {
            Message = errorMessage,
            IsSuccess = false
        };
    }
}