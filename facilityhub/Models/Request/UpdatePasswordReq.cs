namespace FacilityHub.Models.Request;

public class UpdatePasswordReq
{
    public string CurrentPassword { get; set; } = null!;

    public string Password { get; set; } = null!;
}
