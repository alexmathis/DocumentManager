using FluentValidation;

namespace DocumentManager.Application.Organizations.Commands.CreateOrganization;

public sealed class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        // Validate Name: must not be empty and should have a reasonable max length
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Organization name cannot be empty.")
            .MaximumLength(100).WithMessage("Organization name cannot exceed 100 characters.");

        // Validate CreatingUserId: must be a valid positive integer
        RuleFor(x => x.CreatingUserId)
            .GreaterThan(0).WithMessage("Creating user ID must be a positive integer.");
    }
}
