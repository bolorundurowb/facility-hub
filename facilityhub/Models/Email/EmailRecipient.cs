namespace FacilityHub.Models.Email;

public class EmailRecipient
{
    public string? Name { get; }

    public string Email { get; }

    public EmailRecipient(string email, string? name = null)
    {
        Email = email;
        Name = name;
    }
}
