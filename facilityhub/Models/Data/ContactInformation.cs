namespace FacilityHub.Models.Data;

public class ContactInformation
{
    public string Name { get; private set; }

    public string PhoneNumber { get; private set; }

#pragma warning disable CS8618
    private ContactInformation() { }
#pragma warning restore CS8618

    public ContactInformation(string name, string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}
