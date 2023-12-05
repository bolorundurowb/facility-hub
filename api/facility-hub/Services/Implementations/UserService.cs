using FacilityHub.Models.Data;
using FacilityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub.Services.Implementations;

public class UserService : IUserService
{
    private readonly FacilityHubDbContext _dbContext;

    public UserService(FacilityHubDbContext dbContext) => _dbContext = dbContext;

    public Task<User?> FindByEmail(string emailAddress)
    {
        var normalizedEmailAddress = emailAddress.Trim().ToLowerInvariant();
        return _dbContext.Users
            .FirstOrDefaultAsync(x => x.EmailAddress == normalizedEmailAddress);
    }

    public async Task<User> Create(string? firstName, string? lastName, string emailAddress, string password)
    {
        var user = new User(emailAddress, password, firstName, lastName);
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}
