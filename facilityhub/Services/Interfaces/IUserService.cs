using FacilityHub.Models.Data;

namespace FacilityHub.Services.Interfaces;

public interface IUserService
{
    Task<User?> FindByEmail(string emailAddress);

    Task<User?> FindById(Guid userId);

    Task<User> Create(string? firstName, string? lastName, string emailAddress, string password);

    Task RequestPasswordReset(string emailAddress);

    Task ResetPassword(User user, string password);

    Task<User> Update(User user, string? firstName, string? lastName, string? phoneNumber);

    Task UpdatePassword(User user, string password);
}
