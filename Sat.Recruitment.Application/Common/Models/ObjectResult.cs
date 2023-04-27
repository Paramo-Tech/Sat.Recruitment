namespace Sat.Recruitment.Application.Common.Models
{
    public class ObjectResult<T> : Result where T : class
    {
        public T Result { get; set; } = null!;
    }
}
