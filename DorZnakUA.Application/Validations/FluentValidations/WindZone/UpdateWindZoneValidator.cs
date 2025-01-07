using Domain.DorZnakUA.Dto.WindZone;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.WindZone;

public class UpdateWindZoneValidator : AbstractValidator<UpdateWindZoneDto>
{
    public UpdateWindZoneValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
    }
}