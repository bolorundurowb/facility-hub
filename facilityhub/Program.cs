using dotenv.net;
using Sentry;

namespace FacilityHub;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                DotEnv.Fluent()
                    .WithDefaultEncoding()
                    .WithProbeForEnv()
                    .WithoutExceptions()
                    .Load();

                webBuilder.UseSentry(opts =>
                {
                    opts.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN") ?? string.Empty;
                    opts.Debug = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production";
                    opts.DiagnosticLevel = SentryLevel.Info;
                    opts.TracesSampleRate = 0.5;
                });
                webBuilder.UseStartup<Startup>();
            })
            .Build()
            .RunAsync();
        ;
    }
}
