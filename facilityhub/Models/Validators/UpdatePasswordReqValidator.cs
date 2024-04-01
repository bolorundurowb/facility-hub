using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class UpdatePasswordReqValidator : AbstractValidator<UpdatePasswordReq>
{
    public UpdatePasswordReqValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
