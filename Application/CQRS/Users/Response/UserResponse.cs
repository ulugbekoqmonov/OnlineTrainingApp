using Domain.Models.Entities;

namespace Application.CQRS.Users.Response;

public record UserResponse
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<Role>? Roles { get; set; }
}
