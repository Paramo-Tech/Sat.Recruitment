namespace Sat.Rec.Core.Validation
{
    public class ValidationResult<T>
    {
        public int CustomResultCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public T? SingleResult { get; set; }
        public List<T> ResultList { get; set; } = new List<T>();
    }
}
