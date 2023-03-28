namespace Sat.Recruitment.Core.Interfaces
{
    public interface IErrorFactory
    {
        IResponseResult<string> CreateError(string method, Exception ex, string? message = null);
    }
}
