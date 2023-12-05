namespace FacilityHub.Models.Response;

public record UserRes(Guid Id, string? FirstName, string? LastName, string EmailAddress, DateTimeOffset JoinedAt);
