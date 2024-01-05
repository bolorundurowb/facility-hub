using FacilityHub.Enums;

namespace FacilityHub.Models.Response;

public class IssueRes
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Details { get; set; } = null!;

    public IssueStatus Status { get; set; }

    public DateTimeOffset FiledAt { get; set; }
}
