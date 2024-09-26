using DocumentManager.Domain.Interfaces;
using MediatR;

namespace DocumentManager.Application.Organizations.Commands.DeleteOrganization;


public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, Unit>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the organization by Id
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            throw new KeyNotFoundException($"Organization with Id {request.Id} not found.");
        }

        // Delete the organization
        _organizationRepository.Delete(organization);

        // Save changes to the database
        await _organizationRepository.SaveChangesAsync();

        return Unit.Value; // Return Unit to indicate success
    }
}