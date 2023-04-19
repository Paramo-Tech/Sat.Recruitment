namespace Sat.Recruitment.Api.Responses
{
    public class Result<T>
    {
        public Result(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
