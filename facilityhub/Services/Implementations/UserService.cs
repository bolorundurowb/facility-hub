using FacilityHub.DataContext;
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

    public Task<User?> FindById(Guid userId)
    {
        return _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User> Create(string? firstName, string? lastName, string emailAddress, string password)
    {
        var user = new User(emailAddress, password, firstName, lastName);
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task RequestPasswordReset(string emailAddress)
    {
        var normalizedEmail = emailAddress.ToLowerInvariant();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.EmailAddress == normalizedEmail);

        // if an account does not exist, do not let the caller know for security reasons
        if (user != null)
        {
            user.SetResetCode();
            await _dbContext.SaveChangesAsync();

            // TODO: send email to the user (include the user id and reset code)
        }
    }

    public async Task ResetPassword(User user, string password)
    {
        user.UpdatePassword(password);
        await _dbContext.SaveChangesAsync();

        // TODO: send email to the user letting them know their password has changed
    }

    public async Task<User> Update(User user, string? firstName, string? lastName, string? phoneNumber)
    {
        user.UpdateFirstName(firstName);
        user.UpdateLastName(lastName);
        user.UpdatePhoneNumber(phoneNumber);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}
