using System.ComponentModel.DataAnnotations;
using FacilityHub.Enums;
using shortid;
using shortid.Configuration;

namespace FacilityHub.Models.Data;

public class Issue : Entity
{
    private static readonly GenerationOptions
        GenOptions = new(useNumbers: true, useSpecialCharacters: false, length: 8);

    [StringLength(8)]
    public string Code { get; private set; }

    [StringLength(256)]
    public string Title { get; private set; }

    [StringLength(4096)]
    public string Details { get; private set; }

    public IssueStatus Status { get; private set; }

    public List<Document> Documents { get; set; } = new();

    public List<IssueLogEntry> Log { get; private set; } = new();

    public Tenant FiledBy { get; private set; }

    public DateTimeOffset FiledAt { get; private set; }

#pragma warning disable CS8618
    private Issue() { }
#pragma warning restore CS8618

    public Issue(Tenant filedBy, string title, string details)
    {
        FiledBy = filedBy;
        Title = title;
        Details = details;

        Code = ShortId.Generate(GenOptions);
        Status = IssueStatus.Filed;
        Log = new List<IssueLogEntry> { new(null, Status, null) };
        FiledAt = DateTimeOffset.Now;
    }
}
