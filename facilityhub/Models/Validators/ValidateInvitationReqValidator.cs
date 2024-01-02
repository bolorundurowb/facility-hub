using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class ValidateInvitationReqValidator : AbstractValidator<ValidateInvitationReq>
{
    public ValidateInvitationReqValidator()
    {
        RuleFor(x => x.ClaimToken)
            .NotEqual(x => Guid.Empty)
            .WithMessage("A claim token is required");
    }
}
