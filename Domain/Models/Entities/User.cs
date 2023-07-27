using Domain.Models.Common;

namespace Domain.Models.Entities;

public class User:BaseAuditableEntity
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImageName { get; set; }

    public virtual ICollection<Role>? Roles { get; set; }
}
