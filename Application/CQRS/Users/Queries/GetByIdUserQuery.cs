using Application.Abstraction;
using Application.Exceptions;
using AutoMapper;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Queries;

public record GetByIdUserQuery:IRequest<GetByIdUserQueryResponse>
{
    public Guid Id { get; set; }
}
public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetByIdUserQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(request.Id));
        if(user is null)
        {
            throw new NotFoundException(request.Id.ToString());
        }
        GetByIdUserQueryResponse response = _mapper.Map<GetByIdUserQueryResponse>(user);
        return response;
    }
}

public record GetByIdUserQueryResponse
{
    public Guid Id { get; set; }
    public string? Fullname { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<Role>? Roles { get; set; }
}