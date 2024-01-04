using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class FacilityInvitationReqValidator : AbstractValidator<FacilityInvitationReq>
{
    public FacilityInvitationReqValidator()
    {
        RuleFor(x => x.FacilityId)
            .NotEqual(x => Guid.Empty)
            .WithMessage("A facility ID is required");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("An email address is required")
            .EmailAddress()
            .WithMessage("Invalid email address");
    }
}
