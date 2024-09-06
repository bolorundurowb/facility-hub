using System.Reflection;
using dotenv.net.Utilities;
using FacilityHub.Models.Email;
using HandlebarsDotNet;

namespace FacilityHub.Helpers;

public static class EmailTemplateHelpers
{
    private static readonly string FrontendUrl = EnvReader.GetStringValue("FRONTEND_URL");

    public static async Task<EmailMessage> GetForgotPasswordEmailAsync(string? firstName, Guid userId, string resetCode)
    {
        const string templateName = "ForgotPassword";
        var payload = new
        {
            FirstName = firstName ?? "There",
            ResetUrl = $"{FrontendUrl}/auth/reset-password?user-ref={userId:D}&reset-code={resetCode}"
        };

        var html = await GetTemplateAsync(templateName, payload);
        return new EmailMessage("Your password reset code", html);
    }
    
    public static async Task<EmailMessage> GetPasswordChangedEmailAsync(string? firstName)
    {
        const string templateName = "PasswordChanged";
        var payload = new
        {
            FirstName = firstName ?? "There"
        };

        var html = await GetTemplateAsync(templateName, payload);
        return new EmailMessage("Your password has been changed", html);
    }
    
    public static async Task<EmailMessage> GetFacilityContributorInvitationEmailAsync(string? firstName, string? inviterName, string facilityName, string invitationType)
    {
        const string templateName = "FacilityContributorInvitation";
        var payload = new
        {
            FirstName = firstName ?? "There",
            InviterName = inviterName ?? "Someone",
            FacilityName = facilityName,
            InvitationType = invitationType,
            AppUrl = FrontendUrl
        };

        var html = await GetTemplateAsync(templateName, payload);
        return new EmailMessage("You have been invited to a facility", html);
    }

    private static async Task<string> GetTemplateAsync(string templateName, dynamic data)
    {
        var assembly = Assembly.GetExecutingAssembly();
        const string rootTemplateNamespace = "FacilityHub.Templates.RootEmailTemplate.hbs";
        var templateNamespace = $"FacilityHub.Templates.{templateName}.hbs";

        await using var rootStream = assembly.GetManifestResourceStream(rootTemplateNamespace);
        await using var templateStream = assembly.GetManifestResourceStream(templateNamespace);

        if (rootStream == null)
            return string.Empty;

        if (templateStream == null)
            return string.Empty;

        using StreamReader rootStreamReader = new(rootStream),
            streamReader = new(templateStream);
        var rootTemplate = Handlebars.Compile(await rootStreamReader.ReadToEndAsync());
        var subTemplate = Handlebars.Compile(await streamReader.ReadToEndAsync());
        var htmlToEmbed = subTemplate(data);
        return rootTemplate(new { Body = htmlToEmbed });
    }
}
