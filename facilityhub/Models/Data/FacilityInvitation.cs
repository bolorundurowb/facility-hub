using System.ComponentModel.DataAnnotations;
using FacilityHub.Enums;

namespace FacilityHub.Models.Data;

public class FacilityInvitation : Entity
{
    public Facility Facility { get; private set; }

    public FacilityInvitationType Type { get; private set; }

    public Guid ClaimToken { get; private set; }

    public bool IsClaimed { get; private set; }

    [StringLength(256)]
    public string EmailAddress { get; private set; }

    public DateTimeOffset ExpiresAt { get; private set; }

    public User InvitedBy { get; private set; }

    public DateTimeOffset InvitedAt { get; private set; }

#pragma warning disable CS8618
    private FacilityInvitation() { }
#pragma warning restore CS8618

    public FacilityInvitation(Facility facility, User invitedBy, FacilityInvitationType type, string emailAddress)
    {
        Facility = facility;
        InvitedBy = invitedBy;
        Type = type;
        EmailAddress = emailAddress;

        IsClaimed = false;
        InvitedAt = DateTimeOffset.UtcNow;

        GenerateClaimDetails();
    }

    public void GenerateClaimDetails()
    {
        if (IsClaimed)
            throw new InvalidOperationException();

        ClaimToken = Guid.NewGuid();
        ExpiresAt = DateTimeOffset.UtcNow.AddDays(7);
    }

    public bool IsExpired() => ExpiresAt < DateTimeOffset.UtcNow;

    public void Claim() => IsClaimed = true;
}
