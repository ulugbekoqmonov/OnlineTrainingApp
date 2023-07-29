using Application.Abstraction;
using Application.Exceptions;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Permissions.Commands.DeletePermission;

public record DeletePermissionCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeletePermissionCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
        if (permission == null)
        {
            throw new NotFoundException(nameof(Permission));
        }
        else
        {
            _dbContext.Permissions.Remove(permission);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
