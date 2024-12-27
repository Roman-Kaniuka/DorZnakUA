using Domain.DorZnakUA.Dto.Shield;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.Shield;

public class CreateShieldValidator : AbstractValidator<CreateShieldDto>
{
    public CreateShieldValidator()
    {
        RuleFor(x => x.Group).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Shape).NotEmpty().MaximumLength(50);
        RuleFor(x => x.SizeType).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Height).NotEmpty();
        RuleFor(x => x.Width).NotEmpty();
        RuleFor(x => x.Weight).NotEmpty();
    }
}
