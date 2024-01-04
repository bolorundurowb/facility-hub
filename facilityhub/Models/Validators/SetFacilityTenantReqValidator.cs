using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class SetFacilityTenantReqValidator : AbstractValidator<SetFacilityTenantReq>
{
    public SetFacilityTenantReqValidator()
    {
        RuleFor(x => x.FacilityId)
            .NotEqual(x => Guid.Empty)
            .WithMessage("A facility ID is required");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("An email address is required")
            .EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(x => x.StartsAt)
            .NotEqual(DateOnly.MinValue)
            .WithMessage("A tenancy start time must be set");

        RuleFor(x => x.EndsAt)
            .NotEqual(DateOnly.MinValue)
            .WithMessage("A tenancy end time must be set")
            .GreaterThan(x => x.StartsAt)
            .WithMessage("The end time cannot be before the start time");

        RuleFor(x => x.PaidAt)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.Date))
            .WithMessage("The payment time cannot be in the future");
    }
}
