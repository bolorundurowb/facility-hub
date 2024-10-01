namespace FacilityHub.Models.Email;

public class EmailRecipient(string email, string? name = null)
{
    public string? Name { get; } = name;

    public string Email { get; } = email;
}
