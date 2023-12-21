namespace FacilityHub.Models.Data;

public class Tenant : Entity
{
    public User User { get; private set; }

    public List<Document> Documents { get; set; }

    public List<TenancyHistory> History { get; private set; }

    public User CreatedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

#pragma warning disable CS8618
    private Tenant() { }
#pragma warning restore CS8618

    public Tenant(User creator, User user, DateTimeOffset periodStart, DateTimeOffset periodEnd, DateTimeOffset paidAt)
    {
        User = user;
        History = new List<TenancyHistory> { new(periodStart, periodEnd, paidAt) };

        CreatedBy = creator;
        CreatedAt = DateTimeOffset.Now;
    }
}
