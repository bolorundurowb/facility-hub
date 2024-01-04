namespace FacilityHub.Models.Response;

public record FacilityRes(Guid Id, string Name, TenantRes? Tenant, List<DocumentRes> Documents);
