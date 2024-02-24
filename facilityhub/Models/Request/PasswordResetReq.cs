namespace FacilityHub.Models.Request;

public class PasswordResetReq
{
    public string ResetCode { get; set; } = null!;

    public string Password { get; set; } = null!;
}
