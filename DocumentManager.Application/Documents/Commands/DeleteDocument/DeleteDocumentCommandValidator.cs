using FluentValidation;


namespace DocumentManager.Application.Documents.Commands.DeleteDocument;

public sealed class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteDocumentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Document ID must be a valid positive integer.");

        RuleFor(x => x.DeletingUserId)
            .GreaterThan(0).WithMessage("Deleting User required");
    }
}