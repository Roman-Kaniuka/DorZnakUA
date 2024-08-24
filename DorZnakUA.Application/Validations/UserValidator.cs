using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Validations;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;

namespace DorZnakUA.Application.Validations;

public class UserValidator : IBaseValidator <User>
{
    public BaseResult ValidateOnNull(User? model)
    {
        if (model==null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErroreCode = (int) ErrorCodes.UserNotFound,
            };
        }

        return new BaseResult();
    }
}