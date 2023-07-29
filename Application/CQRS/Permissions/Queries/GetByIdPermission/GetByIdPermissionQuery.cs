using Application.Abstraction;
using Application.Exceptions;
using AutoMapper;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Permissions.Queries.GetByIdPermission;

public record GetByIdPermissionQuery:IRequest<GetByIdPermissionResponse>
{
    public Guid Id { get; set; }
}

public class GetByIdPermissionQueryHandler : IRequestHandler<GetByIdPermissionQuery, GetByIdPermissionResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetByIdPermissionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetByIdPermissionResponse> Handle(GetByIdPermissionQuery request, CancellationToken cancellationToken)
    {
        Permission? permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
        if(permission is null)
        {
            throw new NotFoundException(nameof(Permission));
        }
        var permissionResponse = _mapper.Map<GetByIdPermissionResponse>(permission);
        return permissionResponse;
    }
}

public record GetByIdPermissionResponse
{
    public Guid Id { get; set; }
    public string? PermissionName { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }
}
