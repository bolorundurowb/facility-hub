using dotenv.net.Utilities;

namespace FacilityHub;

public static class Config
{
    public static string DbUrl => EnvReader.GetStringValue("DB_URL");

    public static string Secret => EnvReader.GetStringValue("SECRET");

    public static string Audience => "facilityhub.africa";

    public static string Issuer => "facilityhub.africa";
    
    public const long MaxDocumentSize = 10 * 1024 * 1024; // 10MB

    public static readonly string[] AcceptedPhotoFileExtensions = { ".jpg", ".jpeg", ".png", ".svg", ".webp" };

    public static readonly string[] AcceptedDocumentFileExtensions =
        { ".jpg", ".jpeg", ".png", ".doc", ".docx", ".pdf", ".xls", ".xlsx" };
}
