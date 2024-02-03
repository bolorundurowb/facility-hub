using System.Reflection;
using FacilityHub.Models.Email;
using HandlebarsDotNet;

namespace FacilityHub.Helpers;

public static class EmailTemplateHelpers
{
    public static async Task<EmailMessage> GetForgotPasswordEmailAsync(string firstName, string resetCode)
    {
        const string templateName = "ForgotPassword";
        var payload = new
        {
            FirstName = firstName,
            ResetCode = resetCode
        };

        var html = await GetTemplateAsync(templateName, payload);
        return new EmailMessage("Your password reset code", html, null, null, null);
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
        return rootTemplate(new
        {
            Body = htmlToEmbed
        });
    }
}