using System.Text;
using dotenv.net.Utilities;

namespace FacilityHub;

public static class Config
{
    private static readonly Random Random = new();

    public static string DbUrl => EnvReader.GetStringValue("DATABASE_URL");

    public static string Secret => EnvReader.GetStringValue("SECRET");

    public static string Audience => "facilityhub.africa";

    public static string Issuer => "facilityhub.africa";

    public const long MaxDocumentSize = 10 * 1024 * 1024; // 10MB

    public static readonly string[] AcceptedPhotoFileExtensions = { ".jpg", ".jpeg", ".png", ".svg", ".webp" };

    public static readonly string[] AcceptedDocumentFileExtensions =
        { ".jpg", ".jpeg", ".png", ".doc", ".docx", ".pdf", ".xls", ".xlsx" };

    public static string GenerateCode(int length = 6)
    {
        const string source = "12346789ABCDEFGHJKLMNPQRTUVWXYZ";
        var output = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var index = Random.Next(0, source.Length);
            output.Append(source[index]);
        }

        return output.ToString();
    }
}
