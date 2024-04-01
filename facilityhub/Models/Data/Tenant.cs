using System.ComponentModel.DataAnnotations;

namespace FacilityHub.Models.Data;

public class Tenant : Entity
{
    [StringLength(512)]
    public string Name { get; private set; }

    [StringLength(256)]
    public string EmailAddress { get; private set; }

    [StringLength(256)]
    public string? PhoneNumber { get; private set; }

    public User? User { get; private set; }

    public List<Document> Documents { get; set; } = new();

    public List<TenancyHistory> History { get; private set; }

    public User CreatedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

#pragma warning disable CS8618
    private Tenant() { }
#pragma warning restore CS8618

    public Tenant(User creator, string name, string emailAddress, string? phoneNumber, DateOnly periodStart,
        DateOnly periodEnd, DateOnly paidAt,
        User? user = null)
    {
        User = user;
        Name = name;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
        History = new List<TenancyHistory> { new(periodStart, periodEnd, paidAt) };

        CreatedBy = creator;
        CreatedAt = DateTimeOffset.Now;
    }

    public void SetUser(User user) => User = user;
}
