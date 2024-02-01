namespace FacilityHub.Models.Email;

public class EmailRecipient
{
    public string Name { get; }

    public string Email { get; }

    public EmailRecipient(string name, string email)
    {
        Name = name;
        Email = email;
    }
}