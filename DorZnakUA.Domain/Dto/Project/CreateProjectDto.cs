namespace Domain.DorZnakUA.Dto.Project;

public record CreateProjectDto(string Name, string Description, long UserId, long WindZoneId);