using Application.CQRS.Roles.Queries.GetByIdRole;
using Application.CQRS.Roles.Response;
using AutoMapper;
using Domain.Models.Entities;

namespace Application.Mappings;

public class RoleMapping:Profile
{
    public RoleMapping()
    {
        CreateMap<List<RoleResponse>,List<Role>>().ReverseMap();
        CreateMap<GetByIdRoleQueryResponse,Role>().ReverseMap();
    }
}
