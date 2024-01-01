namespace FacilityHub.Models.Response;

public record FacilityRes(Guid Id, string Name, List<DocumentRes> Documents);
