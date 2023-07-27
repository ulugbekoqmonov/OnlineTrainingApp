using Microsoft.EntityFrameworkCore;
using Application.Abstraction;
using Domain.Models.Entities;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Roles.Queries.GetByIdRole;

public record GetByIdRoleQuery : IRequest<GetByIdRoleQueryResponse>
{
    public Guid Id { get; set; }
}
public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, GetByIdRoleQueryResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetByIdRoleQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetByIdRoleQueryResponse> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
    {
        var roleResponse = new GetByIdRoleQueryResponse();
        var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id.Equals(request.Id));

        if (role == null)
        {
            throw new NotFoundException(nameof(Role));
        }
        else
        {
            roleResponse = _mapper.Map<GetByIdRoleQueryResponse>(role);
        }

        return roleResponse;
    }
}
public record GetByIdRoleQueryResponse
{
    public Guid Id { get; set; }
    public string? RoleName { get; set; }

    public virtual ICollection<User>? Users { get; set; }
    public virtual ICollection<Permission>? Permissions { get; set; }
}