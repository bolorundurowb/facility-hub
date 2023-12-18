namespace FacilityHub.Models.Response;

public record AuthRes(string Token, DateTimeOffset ExpiresAt, UserRes User);
