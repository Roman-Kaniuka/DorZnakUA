using AutoMapper;
using Domain.DorZnakUA.Dto.User;
using Domain.DorZnakUA.Entity;

namespace DorZnakUA.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>()
            .ForCtorParam(ctorParamName:"Login",m=>m.MapFrom(s =>s.Login))
            .ReverseMap();

    }
}