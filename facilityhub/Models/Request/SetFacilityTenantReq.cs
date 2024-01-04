namespace FacilityHub.Models.Request;

public class SetFacilityTenantReq : FacilityInvitationReq
{
    public DateOnly StartsAt { get; set; }

    public DateOnly EndsAt { get; set; }

    public DateOnly PaidAt { get; set; }
}
