namespace Sat.Recruitment.Domain.Interfaces
{
    public interface IValidableDto
    {
        Task<string> ValidateDto();
    }
}
