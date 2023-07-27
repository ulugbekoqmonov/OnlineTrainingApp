using Application.Abstraction;
using Application.Exceptions;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Roles.Commands.UpdateRole;

public record UpdateRoleCommand:IRequest
{
    public Guid Id { get; set; }
    public string? RoleName { get; set; }
    public virtual List<Guid>? UserIds { get; set; }
    public virtual List<Guid>? PermissionIds { get; set; }
}

public class updateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public updateRoleCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _dbContext.Roles.FirstOrDefaultAsync(r =>r.Id.Equals(request.Id), cancellationToken);
        if(role is null)
        {
            throw new NotFoundException(nameof(Role));
        }
        else
        {
            List<User> users = new List<User>();
            if (request.UserIds.Count > 0)
            {
                role.Users.Clear();
                request.UserIds.ForEach(async id =>
                {
                    User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id), cancellationToken);
                    if (user is not null)
                    {
                        users.Add(user);
                    }
                });
            }
            List<Permission> permissions = new List<Permission>();
            if (request.PermissionIds.Count > 0)
            {
                role.Permissions.Clear();
                request.PermissionIds.ForEach(async id =>
                {
                    Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
                    if (permission is not null)
                    {
                        permissions.Add(permission);
                    }
                });
            }
            Role roleEntity = new Role
            {
                Id = role.Id,
                RoleName = request.RoleName,
                Users=users,
                Permissions=permissions
            };
            _dbContext.Roles.Update(roleEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }        
    }
}
