namespace Core.Baseclasses
{
    public abstract class CommonResponse : IApplicationResponse
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
