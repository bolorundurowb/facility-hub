using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class CreateFacilityReqValidator : AbstractValidator<CreateFacilityReq>
{
    public CreateFacilityReqValidator()
    {
        RuleFor(req => req.Name)
            .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(req => req.Address)
            .NotEmpty().WithMessage("Address cannot be empty");

        RuleFor(req => req.Location)
            .SetValidator(new LocationDtoValidator()!) 
            .When(req => req.Location != null);
    }
}
