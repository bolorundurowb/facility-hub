namespace FacilityHub.Models.Response;

public class FacilityRes
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public TenantRes? Tenant { get; set; }

    public bool IsTenant { get; set; }
}
