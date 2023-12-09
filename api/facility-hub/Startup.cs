using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FacilityHub.Services.Implementations;
using FacilityHub.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
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
                .UseMySql(
                    Config.DbUrl,
                    new MySqlServerVersion(Version.Parse("8.2.0")),
                    opts => opts.UseNetTopologySuite()
                )
#if DEBUG
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
#endif
        );

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddScoped<IUserService, UserService>();
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
