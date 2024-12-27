using Domain.DorZnakUA.Dto.MetalRack;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.MetalRack;

public class UpdateMEtalRackValidator : AbstractValidator<UpdateMetalRackDto>
{
    public UpdateMEtalRackValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Height).NotEmpty();
        RuleFor(x => x.Weight).NotEmpty();
        RuleFor(x => x.Diameter).NotEmpty();
        RuleFor(x => x.Thickness).NotEmpty();
    }
}