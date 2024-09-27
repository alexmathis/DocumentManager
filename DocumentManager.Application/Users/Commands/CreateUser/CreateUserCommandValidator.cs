using FluentValidation;

namespace DocumentManager.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.OrganizationId)
            .GreaterThan(0).WithMessage("Organization ID must be a positive integer.");
    }
}
