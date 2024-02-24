using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class PasswordResetReqValidator: AbstractValidator<PasswordResetReq>
{
    public PasswordResetReqValidator()
    {
        RuleFor(x => x.ResetCode)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
