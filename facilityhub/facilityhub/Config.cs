using dotenv.net.Utilities;

namespace FacilityHub;

public static class Config
{
    public static string DbUrl => EnvReader.GetStringValue("DB_URL");

    public static string Secret => EnvReader.GetStringValue("SECRET");

    public static string Audience => "facilityhub.africa";

    public static string Issuer => "facilityhub.africa";
}
