using Application.CQRS.Permissions.Queries.GetAllPermissions;
using Application.CQRS.Permissions.Queries.GetByIdPermission;
using Application.CQRS.Permissions.Response;
using AutoMapper;

namespace Application.Mappings;

public class PermissionMapping : Profile
{
    public PermissionMapping()
    {
        CreateMap<GetByIdPermissionQuery, GetByIdPermissionResponse>().ReverseMap();
        CreateMap<List<GetAllPermissionsQuery>, List<PermissionResponse>>().ReverseMap();
    }
}
