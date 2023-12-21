namespace FacilityHub.Models.Data;

public class TenancyHistory
{
    public DateTimeOffset PeriodStart { get; private set; }

    public DateTimeOffset PeriodEnd { get; private set; }

    public DateTimeOffset PaidAt { get; private set; }

#pragma warning disable CS8618
    private TenancyHistory() { }
#pragma warning restore CS8618

    public TenancyHistory(DateTimeOffset periodStart, DateTimeOffset periodEnd, DateTimeOffset paidAt)
    {
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        PaidAt = paidAt;
    }
}
