using MediatR;

namespace Application.CQRS.User.CreateUserCommand;

public class CreateUserCommand:IRequest<CreateUserCommandResponse>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class CreateUserCommandResponse
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }    
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}