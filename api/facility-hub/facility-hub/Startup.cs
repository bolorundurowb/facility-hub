using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using dotenv.net;
using FacilityHub.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FacilityHub;

public class Startup
{
    private readonly IWebHostEnvironment _environment;

    public Startup(IWebHostEnvironment environment) => _environment = environment;

    public void ConfigureServices(IServiceCollection services)
    {
        if (_environment.IsDevelopment())
            DotEnv.Fluent()
                .WithDefaultEncoding()
                .WithProbeForEnv()
                .WithExceptions()
                .Load();

        services.AddCors();
        services.AddRouting(option => option.LowercaseUrls = true);
        services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation(opts => opts.DisableDataAnnotationsValidation = true);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Config.Audience,
                    ValidIssuer = Config.Issuer,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.Secret))
                };
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var xmlPath = Path.Combine(baseDirectory, "facility-hub.xml");

            options.IncludeXmlComments(xmlPath);
        });

        services.AddDbContext<FacilityHubDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySQL(Config.DbUrl)
#if DEBUG
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
#endif
        );
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseCors(options => options
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseSentryTracing();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
