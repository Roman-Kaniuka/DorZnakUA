namespace Domain.DorZnakUA.Enum;

public enum ErrorCodes
{
    //Project 1-10
    ProjectsNotFound = 0,
    ProjectNotFound = 1,
    ProjectAlreadyExists = 2,
    
    //User 11-20
    UserNotFound = 11, 
    UserAlreadyExists= 12,
    
    //Password 21-30
    PasswordNotEqualsPasswordConfirm = 21,
    
    //Server 31-40
    InternalServerError = 31,
    RegistrationFailed = 32,
    
}