using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class FacilityInvitationReqValidation : AbstractValidator<FacilityInvitationReq>
{
    public FacilityInvitationReqValidation()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("An email address is required")
            .EmailAddress()
            .WithMessage("Invalid email address");
    }
}
