namespace FacilityHub.Models.Response;

public record TenantRes(Guid Id, string? Name, DateOnly? StartsAt, DateOnly? EndsAt);
