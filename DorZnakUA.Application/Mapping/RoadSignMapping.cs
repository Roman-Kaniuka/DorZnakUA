using AutoMapper;
using Domain.DorZnakUA.Dto.RoadSign;
using Domain.DorZnakUA.Entity;

namespace DorZnakUA.Application.Mapping;

public class RoadSignMapping : Profile
{
    public RoadSignMapping()
    {
        CreateMap<RoadSign, RoadSignDto>()
            .ForCtorParam(ctorParamName:"Id",m=>m.MapFrom(s =>s.Id))
            .ForCtorParam(ctorParamName:"Positioning",m=>m.MapFrom(s =>s.Positioning))
            .ForCtorParam(ctorParamName:"PlacementOnRoad",m=>m.MapFrom(s =>s.PlacementOnRoad))
            .ForCtorParam(ctorParamName:"NumberOfRacks",m=>m.MapFrom(s =>s.NumberOfRacks))
            .ReverseMap();
    }
}