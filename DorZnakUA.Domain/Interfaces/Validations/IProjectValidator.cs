using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Validations;

public interface IProjectValidator : IBaseValidator <Project>
{
    /// <summary>
    /// Для сворення нового проєкту перевіряється чи не існує проєкту з такою назвою в БД
    /// Перевіряється UserId на наявнысть, якщо немає то стоврити звіт не можливо 
    /// </summary>
    /// <param name="project"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Project project, User user);
}