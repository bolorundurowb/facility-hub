namespace FacilityHub.Models.Data;

public class User
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailAddress { get; set; }

    public string PasswordHash { get; set; }

    public DateTimeOffset JoinedAt { get; set; }

    public User(string emailAddress, string password, string? firstName = null, string? lastName = null)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress.Trim().ToLowerInvariant();
        JoinedAt = DateTimeOffset.Now;
        SetPassword(password);
    }

    public void SetPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string password) =>
        !string.IsNullOrWhiteSpace(password) && BCrypt.Net.BCrypt.Verify(password, PasswordHash);
}
