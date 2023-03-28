namespace Sat.Recruitment.Core.Interfaces
{
    public interface IOperationDto
    {
        string Code { get; set; }
        string Message { get; set; }
        int Status { get; set; }
    }
}