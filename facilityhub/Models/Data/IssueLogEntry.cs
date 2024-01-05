using System.ComponentModel.DataAnnotations;
using FacilityHub.Enums;

namespace FacilityHub.Models.Data;

public class IssueLogEntry
{
    public IssueStatus? TransitionedFrom { get; private set; }
    
    public IssueStatus TransitionedTo { get; private set; }
    
    [StringLength(1024)]
    public string? Notes { get; private set; }

#pragma warning disable CS8618
    private IssueLogEntry() { }
#pragma warning restore CS8618

    public IssueLogEntry(IssueStatus? transitionedFrom, IssueStatus transitionedTo, string? notes)
    {
        TransitionedFrom = transitionedFrom;
        TransitionedTo = transitionedTo;
        Notes = notes;
    }
}
