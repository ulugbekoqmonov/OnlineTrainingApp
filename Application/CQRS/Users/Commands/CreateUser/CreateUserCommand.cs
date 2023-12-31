﻿using Application.Abstraction;
using Application.Exceptions;
using Application.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<CreateUserCommandResponse>
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (_applicationDbContext.Users.Any(x => x.UserName == request.UserName))
            throw new AllreadyExistsException(nameof(Users), request.UserName);

        Domain.Models.Entities.User user = new()
        {
            FullName = request.FullName,
            UserName = request.UserName,
            PasswordHash = request.Password.GetHashString(),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        var entry = await _applicationDbContext.Users.AddAsync(user);
        if (entry.State is EntityState.Added)
        {
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        CreateUserCommandResponse response = _mapper.Map<CreateUserCommandResponse>(entry.Entity);
        return response;
    }
}

public record CreateUserCommandResponse
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}