using Application.Abstraction;
using Application.CQRS.Common;
using Application.CQRS.Roles.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Roles.Queries.GetAllRoles;

public record GetAllRolesQuery:IRequest<PaginatedList<RoleResponse>>
{
}
public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, PaginatedList<RoleResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllRolesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoleResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _dbContext.Roles.ToListAsync();
        var mappedRoles = _mapper.Map<List<RoleResponse>>(roles);
        var response = new PaginatedList<RoleResponse>(mappedRoles);
        return response;
    }
}
