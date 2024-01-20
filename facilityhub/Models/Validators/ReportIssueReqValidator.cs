using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class ReportIssueReqValidator : AbstractValidator<ReportIssueReq>
{
    public ReportIssueReqValidator()
    {
        RuleFor(req => req.FacilityId)
            .NotEmpty()
            .WithMessage("FacilityId is required.");

        RuleFor(req => req.OccurredAt)
            .NotEmpty()
            .WithMessage("The time occurred is required.");

        RuleFor(req => req.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(4096)
            .WithMessage("Description must not exceed 4096 characters.");

        RuleFor(req => req.Location)
            .NotEmpty()
            .WithMessage("Location is required.")
            .MaximumLength(256)
            .WithMessage("Location must not exceed 256 characters.");

        RuleFor(req => req.RemedialAction)
            .MaximumLength(512)
            .WithMessage("RemedialAction must not exceed 512 characters.");
    }
}
