using System.ComponentModel.DataAnnotations;

namespace FacilityHub.Models.Data;

public class User : Entity
{
    [StringLength(256)]
    public string? FirstName { get; private set; }

    [StringLength(256)]
    public string? LastName { get; private set; }

    [StringLength(256)]
    public string EmailAddress { get; private set; }

    [StringLength(2048)]
    public string PasswordHash { get; private set; }

    public DateTimeOffset JoinedAt { get; private set; }

    public List<Facility> Owned { get; private set; } = new();

    public List<Facility> Managed { get; private set; } = new();

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

    public string FullName() => $"{FirstName} {LastName}".Trim();
}
