using AutoMapper;
using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Entity;

namespace DorZnakUA.Application.Mapping;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<Project, ProjectDto>()
            .ForCtorParam(ctorParamName:"Id",m=>m.MapFrom(s =>s.Id))
            .ForCtorParam(ctorParamName:"Name",m=>m.MapFrom(s =>s.Name))
            .ForCtorParam(ctorParamName:"Description",m=>m.MapFrom(s =>s.Description))
            .ForCtorParam(ctorParamName:"DateCreated",m=>m.MapFrom(s =>s.CreateAt))
            .ReverseMap();
    }
    
}