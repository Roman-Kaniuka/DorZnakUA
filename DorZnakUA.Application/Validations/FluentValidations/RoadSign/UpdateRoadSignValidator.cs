using Domain.DorZnakUA.Dto.RoadSign;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.RoadSign;

public class UpdateRoadSignValidator : AbstractValidator<UpdateRoadSignDto>
{
    public UpdateRoadSignValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Positioning).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PlacementOnRoad).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NumberOfRacks).NotEmpty();
    }
}