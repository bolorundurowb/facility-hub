namespace FacilityHub.Models.Request;

public class ReadyToRepairReq : IssueStatusChangeReq
{
    public string? RepairerName { get; set; }

    public string? RepairerPhoneNumber { get; set; }
}
