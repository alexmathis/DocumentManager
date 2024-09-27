using FluentValidation;

namespace DocumentManager.Application.Organizations.Commands.UpdateOrganization;

public sealed class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Organization ID must be a positive integer.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Organization name cannot be empty.")
            .MaximumLength(100).WithMessage("Organization name cannot exceed 100 characters.");

        RuleFor(x => x.UpdatingUserId)
            .GreaterThan(0).WithMessage("Updating user ID must be a valid positive integer.");
    }
}
