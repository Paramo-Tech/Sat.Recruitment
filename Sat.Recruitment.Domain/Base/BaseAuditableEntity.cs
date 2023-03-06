namespace Sat.Recruitment.Domain.Base;
public class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}