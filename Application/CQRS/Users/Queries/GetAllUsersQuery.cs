using Application.Abstraction;
using Application.CQRS.Common;
using Application.CQRS.Users.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Queries;

public record GetAllUsersQuery:IRequest<PaginatedList<UserResponse>>
{
}
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.ToListAsync();
        var mappedUsers = _mapper.Map<List<UserResponse>>(users);
        PaginatedList<UserResponse> result = new PaginatedList<UserResponse>(mappedUsers);
        return result;
    }
}
