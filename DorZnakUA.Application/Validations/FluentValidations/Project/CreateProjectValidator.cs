using Domain.DorZnakUA.Dto.Project;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.Project;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
    }
}