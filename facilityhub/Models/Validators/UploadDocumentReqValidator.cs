using FacilityHub.Models.Request;
using FluentValidation;

namespace FacilityHub.Models.Validators;

public class UploadDocumentReqValidator  : AbstractValidator<UploadDocumentReq>
{
    public UploadDocumentReqValidator()
    {
        RuleFor(x => x.DocumentType)
            .IsInEnum();

        RuleFor(x => x.File)
            .NotNull()
            .WithMessage("A file is required.")
            .Must(x => x?.Length > 0)
            .WithMessage("Selected file must not be empty.")
            .Must(x => x?.Length <= Config.MaxDocumentSize)
            .WithMessage("Selected file is too large.")
            .Must(x =>
            {
                var extension = Path.GetExtension(x?.FileName);
                return Config.AcceptedDocumentFileExtensions.Contains(extension);
            })
            .WithMessage("Unsupported document format.");
    }
}
