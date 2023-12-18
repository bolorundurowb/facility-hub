using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class LoginReqValidator : AbstractValidator<LoginReq>
{
    public LoginReqValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
