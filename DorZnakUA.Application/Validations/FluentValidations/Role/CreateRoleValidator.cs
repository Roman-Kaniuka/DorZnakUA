using Domain.DorZnakUA.Dto.Role;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.Role;

public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}