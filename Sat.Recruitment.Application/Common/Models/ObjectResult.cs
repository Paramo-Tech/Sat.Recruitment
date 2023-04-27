namespace Sat.Recruitment.Application.Common.Models
{
    public class ObjectResult<T> : Result
    {
        public T Result { get; set; } = default(T)!; 
    }
}
