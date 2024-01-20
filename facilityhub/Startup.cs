using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using CredentialsInUrlParser;
using FacilityHub.DataContext;
using FacilityHub.Extensions;
using FacilityHub.Services.Implementations;
using FacilityHub.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
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
        services.AddLogging();
        services.AddCors();
        services.AddRouting(option => option.LowercaseUrls = true);
        services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .ConfigureApiBehaviorOptions(opts =>
            {
                opts.InvalidModelStateResponseFactory = context => context.Format();
            });

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
            var xmlPath = Path.Combine(baseDirectory, "facilityhub.xml");

            options.IncludeXmlComments(xmlPath);
        });

        services.AddDbContext<FacilityHubDbContext>(
            dbContextOptions => dbContextOptions
                .UseNpgsql(
                    CIU.Parse(Config.DbUrl).ToNpgsqlConnectionString(),
                    opts => opts.UseNetTopologySuite()
                )
#if DEBUG
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
#endif
        );
        services.AddHostedService<DatabaseMigrationService>();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddMapster();

        services.AddScoped<IFacilityService, FacilityService>();
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMediaHandlerService, CloudinaryService>();
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
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            endpoints.MapFallbackToFile("index.html");
        });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
