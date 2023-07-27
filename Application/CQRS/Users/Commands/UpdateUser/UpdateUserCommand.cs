using Application.Abstraction;
using Application.Exceptions;
using Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
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
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(request.Id));
        if (user is null)
        {
            throw new NotFoundException(request.Id.ToString());
        }
        else if (!user.PasswordHash.Equals(request.Password.GetHashString()))
        {
            throw new InvalidPasswordException(request.Password);
        }
        else
        {
            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.PasswordHash = request.NewPassword.GetHashString();
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
