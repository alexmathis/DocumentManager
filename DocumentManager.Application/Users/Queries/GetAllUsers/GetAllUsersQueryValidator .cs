using FluentValidation;

namespace DocumentManager.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
        RuleFor(x => x.RequstingUserId)
            .GreaterThan(0).WithMessage("Requesting user ID must be a positive integer.");
    }
}
