namespace FacilityHub.Models.Request;

public class SetFacilityTenantReq : FacilityInvitationReq
{
    public string Name { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateOnly StartsAt { get; set; }

    public DateOnly EndsAt { get; set; }

    public DateOnly PaidAt { get; set; }
}
