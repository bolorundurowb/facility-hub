namespace FacilityHub.Extensions;

public static class StringExtensions
{
    public static (string FirstName, string LastName) SplitName(this string fullName)
    {
        var nameParts = fullName.Split(' ');
        var firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
        var lastName = nameParts.Length > 1 ? nameParts[^1] : string.Empty;
        return (firstName, lastName);
    }
}
