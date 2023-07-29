using Application.Abstraction;
using Application.CQRS.Permissions.Response;
using AutoMapper;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Permissions.Queries.GetAllPermissions;

public record GetAllPermissionsQuery:IRequest<List<PermissionResponse>>
{
}

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, List<PermissionResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetAllPermissionsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<PermissionResponse>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        List<Permission> permissions = await _dbContext.Permissions.ToListAsync(cancellationToken);
        var permissionsResult = _mapper.Map<List<PermissionResponse>>(permissions);
        return permissionsResult;
    }
}
