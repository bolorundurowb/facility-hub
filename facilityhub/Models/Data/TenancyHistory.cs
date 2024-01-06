namespace FacilityHub.Models.Data;

public class TenancyHistory
{
    public DateOnly PeriodStart { get; private set; }

    public DateOnly PeriodEnd { get; private set; }

    public DateOnly PaidAt { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

#pragma warning disable CS8618
    private TenancyHistory() { }
#pragma warning restore CS8618

    public TenancyHistory(DateOnly periodStart, DateOnly periodEnd, DateOnly paidAt)
    {
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        PaidAt = paidAt;
        CreatedAt = DateTimeOffset.Now;
    }
}
