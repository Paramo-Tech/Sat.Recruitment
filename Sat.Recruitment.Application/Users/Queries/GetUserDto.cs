using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Application.Users.Queries;

public class GetUserDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public UserType UserType { get; set; }
    public decimal Money { get; set; }
}
