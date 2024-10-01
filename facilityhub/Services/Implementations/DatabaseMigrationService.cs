using FacilityHub.DataContext;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub.Services.Implementations;

public class DatabaseMigrationService(IServiceProvider serviceProvider, ILogger<DatabaseMigrationService> logger)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FacilityHubDbContext>();

        try
        {
            logger.LogInformation("Applying database migrations...");
            await dbContext.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
