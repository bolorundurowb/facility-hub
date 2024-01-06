namespace FacilityHub.Models.Request;

public class FacilityInvitationReq
{
    public Guid FacilityId { get; set; }

    public string EmailAddress { get; set; } = null!;
}
