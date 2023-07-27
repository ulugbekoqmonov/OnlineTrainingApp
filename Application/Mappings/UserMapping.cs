using Application.CQRS.Users.Commands.CreateUser;
using Application.CQRS.Users.Queries;
using Application.CQRS.Users.Response;
using AutoMapper;
using Domain.Models.Entities;

namespace Application.Mappings;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<CreateUserCommandResponse, User>().ReverseMap();
        CreateMap<GetByIdUserQueryResponse, User>().ReverseMap();
        CreateMap<List<UserResponse>,List<User>>().ReverseMap();
    }
}
