using AutoMapper;
using Domain.DorZnakUA.Dto.Role;
using Domain.DorZnakUA.Entity;

namespace DorZnakUA.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>()
            .ForCtorParam(ctorParamName:"Id",m=>m.MapFrom(s =>s.Id))
            .ForCtorParam(ctorParamName:"Name",m=>m.MapFrom(s =>s.Name))
            .ReverseMap();
    }
}