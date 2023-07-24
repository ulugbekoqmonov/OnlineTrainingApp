using Application.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Users.CreateUserCommand;

public record GetAllUsersQuery:IRequest<List<GetAllUsersQueryResponse>>
{
}
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    Task<List<GetAllUsersQueryResponse>> IRequestHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>>.Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _dbContext.Users.ToList();
        var mappedUsers = _mapper.Map<List<GetAllUsersQueryResponse>>(users);
        return Task.FromResult(mappedUsers);
    }
}

public record GetAllUsersQueryResponse
{
    public Guid Id {  get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}