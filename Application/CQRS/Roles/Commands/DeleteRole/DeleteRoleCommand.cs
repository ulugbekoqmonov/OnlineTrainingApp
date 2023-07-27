using Application.Abstraction;
using Application.Exceptions;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Roles.Commands.DeleteRole;

public record DeleteRoleCommand:IRequest
{
    public Guid Id { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteRoleCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role  = await _dbContext.Roles.FirstOrDefaultAsync(r=>r.Id.Equals(request.Id), cancellationToken);
        if(role is null)
        {
            throw new NotFoundException(nameof(role));
        }
        else
        {
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
