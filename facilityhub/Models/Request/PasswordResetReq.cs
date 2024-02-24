namespace FacilityHub.Models.Request;

public class PasswordResetReq
{
    public Guid UserId { get; set; }
    
    public string ResetCode { get; set; } = null!;

    public string Password { get; set; } = null!;
}
