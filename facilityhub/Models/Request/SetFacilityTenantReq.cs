namespace FacilityHub.Models.Request;

public class SetFacilityTenantReq : FacilityInvitationReq
{
    public DateTimeOffset StartsAt { get; set; }

    public DateTimeOffset EndsAt { get; set; }

    public DateTimeOffset PaidAt { get; set; }
}
