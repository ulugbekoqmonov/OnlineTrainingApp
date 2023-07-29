using Application.Abstraction;
using Application.Exceptions;
using AutoMapper;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Permissions.Commands.CreatePermission;

public record CreatePermissionCommand : IRequest
{
    public string? PermissionName { get; set; }
}

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreatePermissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Permissions.AnyAsync(per =>
                        per.PermissionName.Equals(request.PermissionName, StringComparison.OrdinalIgnoreCase),
                        cancellationToken))
        {
            throw new AllreadyExistsException(nameof(Permission), request.PermissionName);
        }
        Permission permission = _mapper.Map<Permission>(request);
        _dbContext.Permissions.Add(permission);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
