using System.ComponentModel.DataAnnotations;
using FacilityHub.Enums;
using shortid;
using shortid.Configuration;

namespace FacilityHub.Models.Data;

public class Issue : Entity
{
    private static readonly GenerationOptions
        GenOptions = new(useNumbers: true, useSpecialCharacters: false, length: 8);

    public Facility Facility { get; private set; }

    [StringLength(8)]
    public string Code { get; private set; }

    public DateTimeOffset OccurredAt { get; private set; }

    [StringLength(4096)]
    public string Description { get; private set; }

    [StringLength(256)]
    public string Location { get; private set; }

    [StringLength(512)]
    public string? RemedialAction { get; private set; }

    public IssueStatus Status { get; private set; }

    public List<Document> Evidence { get; set; } = new();

    public List<IssueLogEntry> Log { get; private set; } = new();

    public Tenant FiledBy { get; private set; }

    public DateTimeOffset FiledAt { get; private set; }

#pragma warning disable CS8618
    private Issue() { }
#pragma warning restore CS8618

    public Issue(Facility facility, DateTimeOffset occurredAt, string description, string location,
        string? remedialAction, Tenant filedBy)
    {
        Facility = facility;
        OccurredAt = occurredAt;
        Description = description;
        Location = location;
        RemedialAction = remedialAction;
        FiledBy = filedBy;

        Status = IssueStatus.Filed;
        FiledAt = DateTimeOffset.Now;
        Evidence = new List<Document>();
        Code = ShortId.Generate(GenOptions);
        Log = new List<IssueLogEntry> { new(null, Status, null) };
    }
}
