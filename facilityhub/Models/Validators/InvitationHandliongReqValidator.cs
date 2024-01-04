using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class InvitationHandliongReqValidator : AbstractValidator<InvitationHandlingReq>
{
    public InvitationHandliongReqValidator()
    {
        RuleFor(x => x.ClaimToken)
            .NotEqual(x => Guid.Empty)
            .WithMessage("A claim token is required");
    }
}
