using FluentValidation;

namespace DocumentManager.Application.Documents.Queries.GetAllDocuments;

public sealed class GetAllDocumentsQueryValidator : AbstractValidator<GetAllDocumentsQuery>
{
    public GetAllDocumentsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");
    }
}
