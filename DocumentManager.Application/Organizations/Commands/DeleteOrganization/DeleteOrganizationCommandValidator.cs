using FluentValidation;

namespace DocumentManager.Application.Organizations.Commands.DeleteOrganization;

public sealed class DeleteOrganizationCommandValidator : AbstractValidator<DeleteOrganizationCommand>
{
    public DeleteOrganizationCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Organization ID must be a positive integer.");

        RuleFor(x => x.DeletingUserId)
            .GreaterThan(0).WithMessage("Deleting user ID must be a positive integer.");
    }
}
