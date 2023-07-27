using Application.Abstraction;
using AutoMapper;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest
{
    public Guid Id { get; set; }
    public string? RoleName { get; set; }
    public virtual List<Guid>? PermissionsId { get; set; }
}
public class CreateroleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateroleCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = await _dbContext.Permissions.ToListAsync();
        var newPermissions = new List<Permission>();
        if (request.PermissionsId.Count > 0)
        {
            permissions.ForEach(p =>
            {
                if (request.PermissionsId.Any(id => p.Id.Equals(id)))
                {
                    newPermissions.Add(p);
                }
            });
        }
        var role = new Role
        {
            RoleName = request.RoleName,
            Permissions = newPermissions
        };
        var roleEntity = await _dbContext.Roles.AddAsync(role, cancellationToken);
        if (roleEntity.State == EntityState.Added)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }        
    }
}
