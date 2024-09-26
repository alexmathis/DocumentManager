using DocumentManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, Unit>
{
    private readonly IOrganizationRepository _organizationRepository;

    public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Unit> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the organization by Id
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            throw new KeyNotFoundException($"Organization with Id {request.Id} not found.");
        }

        // Update the organization's name using the provided public method
        organization.UpdateName(request.Name);

        // Update the organization in the repository
        _organizationRepository.Update(organization);

        // Save changes to the database
        await _organizationRepository.SaveChangesAsync();

        return Unit.Value; // Return Unit to indicate success
    }
}