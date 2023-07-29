using Application.Abstraction;
using Application.Exceptions;
using AutoMapper;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Permissions.Commands.UpdatePermission;

public record UpdatePermissionCommand : IRequest
{
    public Guid Id { get; set; }
    public string? PermissionName { get; set; }
}

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdatePermissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
        if (permission is null)
        {
            throw new NotFoundException(nameof(Permission));
        }
        else
        {
            permission.PermissionName = request.PermissionName;
            _dbContext.Permissions.Update(permission);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
