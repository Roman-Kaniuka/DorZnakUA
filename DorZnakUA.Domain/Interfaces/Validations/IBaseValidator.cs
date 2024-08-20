using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Validations;

public interface IBaseValidator <in T> where T : class
{
    BaseResult ValidateOnNull(T model);
}