using Application.Abstraction;
using Application.Exceptions;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.UpdateUserCommand;

public record UpdateUserCommand:IRequest
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateUserCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id.Equals(request.Id));
        if (user is null)
        {
            throw new NotFoundException(request.Id.ToString());
        }
        else
        {
            var user = new User();           
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
