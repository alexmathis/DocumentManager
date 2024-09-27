using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DocumentManager.Application.Documents.Commands.UpdateDocument;

public sealed class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
{
    public UpdateDocumentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Document name cannot be empty.")
            .MaximumLength(100).WithMessage("Document name cannot exceed 100 characters.");

        When(x => x.File != null, () =>
        {
            RuleFor(x => x.File)
                .Must(BeAValidFile).WithMessage("Invalid file format.")
                .Must(BeWithinSizeLimit).WithMessage("File size cannot exceed 10 MB.");
        });

        RuleFor(x => x.EditingUserId)
            .GreaterThan(0).WithMessage("Editing user ID must be a valid positive integer.");
    }

    private bool BeAValidFile(IFormFile file)
    {
        var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".txt" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(extension);
    }

    private bool BeWithinSizeLimit(IFormFile file)
    {
        return file.Length > 0 && file.Length <= 10 * 1024 * 1024; // 10 MB limit
    }
}
