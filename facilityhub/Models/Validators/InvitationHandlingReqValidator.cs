using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class InvitationHandlingReqValidator : AbstractValidator<InvitationHandlingReq>
{
    public InvitationHandlingReqValidator()
    {
        RuleFor(x => x.ClaimToken)
            .NotEqual(x => Guid.Empty)
            .WithMessage("A claim token is required");
    }
}
