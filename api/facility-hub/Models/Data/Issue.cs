namespace FacilityHub.Models.Data;

public class Issue : Entity
{
    public User FiledBy { get; set; }

    public DateTimeOffset FiledAt { get; set; }

#pragma warning disable CS8618
    private Issue() { }
#pragma warning restore CS8618

    public Issue(User filedBy)
    {
        FiledBy = filedBy;
        FiledAt = DateTimeOffset.Now;
    }
}
