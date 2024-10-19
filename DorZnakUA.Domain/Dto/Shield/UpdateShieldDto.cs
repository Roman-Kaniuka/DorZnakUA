namespace Domain.DorZnakUA.Dto.Shield;

public record UpdateShieldDto(long Id, string Group, string Name, string Shape, string SizeType, double Height, double Width, double Weight);