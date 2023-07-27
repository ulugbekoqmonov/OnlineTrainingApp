using Domain.Models.Common;

namespace Domain.Models.Entities;

public class InnerCategory:BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Question>? Questions { get; set; }
}
