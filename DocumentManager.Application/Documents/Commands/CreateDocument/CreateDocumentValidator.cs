using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DocumentManager.Application.Documents.Commands.CreateDocument;

public sealed class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Document name cannot be empty.")
            .MaximumLength(100).WithMessage("Document name cannot exceed 100 characters.");

        RuleFor(x => x.File)
            .NotNull().WithMessage("A file must be provided.")
            .Must(BeAValidFile).WithMessage("Invalid file format.")
            .Must(BeWithinSizeLimit).WithMessage("File size cannot exceed 10 MB.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a valid positive integer.");

        RuleFor(x => x.OrganizationId)
            .GreaterThan(0).WithMessage("Organization ID must be a valid positive integer.");
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
