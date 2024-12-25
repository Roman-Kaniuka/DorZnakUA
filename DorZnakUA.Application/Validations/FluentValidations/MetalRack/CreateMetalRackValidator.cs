using Domain.DorZnakUA.Dto.MetalRack;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.MetalRack;

public class CreateMetalRackValidator : AbstractValidator<CreateMetalRackDto>
{
    public CreateMetalRackValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(10);
        RuleFor(x => x.Height).NotEmpty();
        RuleFor(x => x.Weight).NotEmpty();
        RuleFor(x => x.Diameter).NotEmpty();
        RuleFor(x => x.Thickness).NotEmpty();
    }
}