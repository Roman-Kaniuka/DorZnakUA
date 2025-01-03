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
    PasswordIsWrong = 22,
    
    //Server 31-40
    InternalServerError = 31,
    RegistrationFailed = 32,
    InvalidInputDataError =33,
    
    //Role 41-50
    RoleAlreadyExists = 41,
    RoleNotFound = 42,
    
    //UserRole 51-60
    UserAlreadyExistsThisRole = 51,
    
    //RoadSign 61-70
    RoadSignsNotFound = 61,
    RoadSignNotFound = 62,
    
    //MetalRack 71-80
    MetalRacksNotFound = 71,
    MetalRackNotFound = 72,
    MetalRackAlreadyExists = 73,
    MetalRackWithSimilarParametersAlreadyExists = 74,
    
    //WindZone 81-90
    WindZonesNotFound = 81,
    WindZoneNotFound = 82,
    WindZoneAlreadyExists = 83,
    
    //Shield 91-100
    ShieldsNotFound = 91,
    ShieldNotFound = 92,
    ShieldAlreadyExists = 93,
    
    //RoadSignSield 101-110
    RoadSignAlreadyHasThisShield = 101,
    
}