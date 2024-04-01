using FacilityHub.Models.Email;

namespace FacilityHub.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendAsync(EmailRecipient recipient, EmailMessage emailMessage);
}