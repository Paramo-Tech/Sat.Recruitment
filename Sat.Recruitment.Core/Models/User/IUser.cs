namespace Sat.Recruitment.Core.Models.User
{
    public interface IUser
    {
        string? Id { get; set; }

        string? UserId { get; set; }

        string? Address { get; set; }

        string? Email { get; set; }

        decimal Money { get; set; }

        string? Name { get; set; }

        string? Phone { get; set; }

        string? UserType { get; set; }
    }
}