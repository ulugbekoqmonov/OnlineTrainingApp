using Domain.Models.Entities;

namespace Application.CQRS.Permissions.Response;

public record PermissionResponse
{
    public Guid Id { get; set; }
    public string? PermissionName { get; set; }    
}
