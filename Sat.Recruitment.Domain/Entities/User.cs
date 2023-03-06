namespace Sat.Recruitment.Domain.Entities;
public class User : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public UserType UserType { get; set; }
    public decimal Money { get; set; }
}