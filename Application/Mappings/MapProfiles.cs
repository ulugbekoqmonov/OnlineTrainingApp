using Application.CQRS.Users.CreateUserCommand;
using AutoMapper;
using Domain.Models.Entities;

namespace Application.Mappings;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<CreateUserCommandResponse, User>().ReverseMap();
    }
}
