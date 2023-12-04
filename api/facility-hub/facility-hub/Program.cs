namespace FacilityHub;

public class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseSentry(opts => { opts.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN"); });
            });
    }
}

