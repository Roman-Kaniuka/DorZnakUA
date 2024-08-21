using Domain.DorZnakUA.Dto.Project;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.Project;

public class CreateProjectValodator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValodator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
    }
}