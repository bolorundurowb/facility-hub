namespace FacilityHub.Models.Data;

public class User
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailAddress { get; set; }

    public string PasswordHash { get; set; }

    public DateTimeOffset JoinedAt { get; set; }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618

    public User(string emailAddress, string password, string? firstName = null, string? lastName = null)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress.Trim().ToLowerInvariant();
        JoinedAt = DateTimeOffset.Now;
        PasswordHash = HashText(password);
    }

    public string HashText(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string password) =>
        !string.IsNullOrWhiteSpace(password) && BCrypt.Net.BCrypt.Verify(password, PasswordHash);
}
