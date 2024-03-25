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

    public List<Document> Documents { get; set; } = new();

    public List<IssueLogEntry> Log { get; private set; } = new();

    public Tenant FiledBy { get; private set; }

    public DateTimeOffset FiledAt { get; private set; }

    public ContactInformation? Repairer { get; private set; }

#pragma warning disable CS8618
    private Issue() { }
#pragma warning restore CS8618

    public Issue(Facility facility, DateTimeOffset occurredAt, string description, string location,
        string? remedialAction)
    {
        Facility = facility;
        OccurredAt = occurredAt;
        Description = description;
        Location = location;
        RemedialAction = remedialAction;
        FiledBy = facility.Tenant!;

        Status = IssueStatus.Filed;
        FiledAt = DateTimeOffset.Now;
        Documents = new List<Document>();
        Code = ShortId.Generate(GenOptions);
        Log = new List<IssueLogEntry> { new(null, Status, null) };
    }

    public void AddDocument(Document document) => Documents.Add(document);

    public bool CanValidate() => Status is IssueStatus.Filed;

    public void Validate(User manager, string? notes)
    {
        if (!CanValidate())
            throw new InvalidOperationException("Only freshly filed issues can be validated");

        TransitionToStatus(manager, IssueStatus.Validated, notes);
    }

    public void ScheduleRepair(User manager, string? notes, string? repairerName, string repairerPhoneNumber)
    {
        TransitionToStatus(manager, IssueStatus.RepairScheduled, notes);
        Repairer = new ContactInformation(repairerName, repairerPhoneNumber);
    }

    public bool CanMarkAsDuplicate() => Status is IssueStatus.Filed;

    public void MarkAsDuplicate(User manager, string? notes)
    {
        if (!CanMarkAsDuplicate())
            throw new InvalidOperationException("Only freshly filed issues can be marked as duplicate");

        TransitionToStatus(manager, IssueStatus.Duplicate, notes);
    }

    public bool CanMarkAsRepaired() => Status is IssueStatus.RepairScheduled;

    public void MarkRepaired(User manager, string? notes) =>
        TransitionToStatus(manager, IssueStatus.Repaired, notes);

    public bool CanClose(User tenantUser) => Status is IssueStatus.Repaired && tenantUser.Id == FiledBy.User?.Id ;

    public void Close(User tenantUser) =>
        TransitionToStatus(tenantUser, IssueStatus.Closed, null);

    #region Private Helpers

    private void TransitionToStatus(User manager, IssueStatus transitionTo, string? notes)
    {
        var previousStatus = Status;
        Status = transitionTo;

        Log ??= new List<IssueLogEntry>();
        Log.Add(new IssueLogEntry(previousStatus, Status, $"Transitioned By: {manager.FullName}\n{notes}"));
    }

    #endregion
}
