using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Validations;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;

namespace DorZnakUA.Application.Validations;

public class ProjectValidator : IProjectValidator 
{
    public BaseResult ValidateOnNull(Project? model)
    {
        if (model==null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ProjectNotFound,
                ErroreCode = (int) ErrorCodes.ProjectNotFound,
            };
        }
        return new BaseResult();
    }

    public BaseResult CreateProjectValidator(Project? project, User? user)
    {
        if (project != null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ProjectAlreadyExists,
                ErroreCode = (int)ErrorCodes.ProjectAlreadyExists,
            };
        }

        if (user==null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErroreCode = (int)ErrorCodes.UserNotFound,
            };
        }

        return new BaseResult();
    }
}