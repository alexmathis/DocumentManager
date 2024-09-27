using FluentValidation;

namespace DocumentManager.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");

        RuleFor(x => x.RequstingUserId)
            .GreaterThan(0).WithMessage("Requesting user ID must be a positive integer.");
    }
}
