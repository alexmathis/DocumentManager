using FluentValidation;

namespace DocumentManager.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.OrganizationId)
            .GreaterThan(0).WithMessage("Organization ID must be a positive integer.");

        RuleFor(x => x.RequestingUserId)
            .GreaterThan(0).WithMessage("Requesting User ID must be a positive integer.");
    }
}
