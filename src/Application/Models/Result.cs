namespace Application.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
        public string Message { get; set; }

        public static Result Success()
        {
            return new Result { IsSuccess = true };
        }

        public static Result Success(string message)
        {
            return new Result { IsSuccess = true, Message = message };
        }

        public static Result Failure(string errors)
        {
            return new Result { IsSuccess = false, Errors = errors };
        }
    }
}