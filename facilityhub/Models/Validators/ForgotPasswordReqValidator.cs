using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class ForgotPasswordReqValidator : AbstractValidator<ForgotPasswordReq>
{
    public ForgotPasswordReqValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("An email address is required")
            .EmailAddress()
            .WithMessage("Invalid email address");
    }
}
