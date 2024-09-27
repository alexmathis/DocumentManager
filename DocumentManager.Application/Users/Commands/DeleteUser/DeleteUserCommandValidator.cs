using FluentValidation;

namespace DocumentManager.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");

        RuleFor(x => x.RequestingUserId)
            .GreaterThan(0).WithMessage("Requesting user ID must be a positive integer.");
    }
}
