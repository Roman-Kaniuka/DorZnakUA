using Domain.DorZnakUA.Dto.Role;
using FluentValidation;

namespace DorZnakUA.Application.Validations.FluentValidations.Role;

public class UpdateRoleValitadot : AbstractValidator<RoleDto>
{
    public UpdateRoleValitadot()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}