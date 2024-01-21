using FacilityHub.Enums;

namespace FacilityHub.Models.Response;

public class IssueRes
{
    public Guid Id { get; set; }

    public Guid? FacilityId { get; set; }

    public string? FacilityName { get; set; }

    public string Code { get; set; } = null!;

    public DateTimeOffset OccurredAt { get; set; }

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? RemedialAction { get; set; }

    public IssueStatus Status { get; set; }

    public Guid? FiledById { get; set; }

    public string? FiledByName { get; set; }

    public DateTimeOffset FiledAt { get; set; }
}
