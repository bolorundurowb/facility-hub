namespace FacilityHub.Models.Request;

public class ReportIssueReq
{
    public Guid FacilityId { get; set; }

    public DateTimeOffset OccurredAt { get; set; }

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? RemedialAction { get; set; }
}
