namespace Sat.Recruitment.Api.Features.Common
{
    public class Result
    {
        protected Result() { }

        public bool IsSuccess { get; protected set; }
        public string Errors { get; protected set; }

        public static Result Failure(string errors) => new Result { Errors = errors };

        public static Result Success() => new Result { IsSuccess = true };

        public static Result<T> Failure<T>(string errors) => new Result<T>(errors);

        public static Result<T> Success<T>(T value) => new Result<T>(value);
    }

    public class Result<T> : Result
    {
        protected internal Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        protected internal Result(string errors) => Errors = errors;

        public T Value { get; protected set; }

        public static implicit operator Result<T>(T value) => new Result<T>(value);

        public void Deconstruct(out bool isSuccess, out string errors, out T value)
        {
            isSuccess = IsSuccess;
            errors = Errors;
            value = Value;
        }
    }
}
