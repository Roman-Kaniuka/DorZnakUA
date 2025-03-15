using Domain.DorZnakUA.Dto.Role;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.Role;

public class UpdateRoleValidator : AbstractValidator<RoleDto>
{
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}