using FluentValidation;

namespace DocumentManager.Application.Documents.Queries.GetDocumentById;

public sealed class GetDocumentByIdQueryValidator : AbstractValidator<GetDocumentByIdQuery>
{
    public GetDocumentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Document ID must be a positive integer.");
    }
}
