using FacilityHub.Models.DTOs;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class LocationDtoValidator : AbstractValidator<LocationDto>
{
    public LocationDtoValidator()
    {
        RuleFor(dto => dto.Longitude)
            .InclusiveBetween(-180.0, 180.0)
            .WithMessage("Longitude must be between -180.0 and 180.0");

        RuleFor(dto => dto.Latitude)
            .InclusiveBetween(-90.0, 90.0)
            .WithMessage("Latitude must be between -90.0 and 90.0");
    }
}
