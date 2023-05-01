using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string UserType { get; set; } = default!;
    public decimal Money { get; set; }
}